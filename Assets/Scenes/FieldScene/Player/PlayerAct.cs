using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //�p�����[�^�[
    private PlayerParameter parameter;
    //�X�e�[�^�[
    private PlayerStater stater;
    //���[�o�[
    private PlayerMover mover;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //�V���[�^�[
    private PlayerShooter shooter;
    //�_���[�W���[
    private PlayerDamager damager;
    //�X�}�b�V���[
    private PlayerSmasher smasher;

    //�R���g���[���[
    private Controller controller;
    //�R���_�[
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //�^�[�Q�b�g
    private MeshCreator meshCreator;
    //UI
    private List<IPlayerUI> playerUIs;
    //private Dictionary<string, List<T>> 

    void Awake()
    {
        parameter = GetComponent<PlayerParameter>();
        stater = GetComponent<PlayerStater>();
        mover = GetComponent<PlayerMover>();
        effecter = GetComponent<MobEffecter>();
        shooter = GetComponent<PlayerShooter>();
        damager = GetComponent<PlayerDamager>();
        smasher = GetComponent<PlayerSmasher>();
        smasher.SetSmasher(parameter.SmashParam.SmashCollider, parameter.SmashParam.DestroyTime);
        smasher.onKilling += OrderOutputIncreasingAdrenaline;

        controller = GetComponent<Controller>();
        controller.onMoving += OrderOutputMoving;
        controller.onFiring += OrderOutputFiring;
        controller.onReloading += OrderOutputReloading;
        controller.onSmashing += OrderOutputSmashing;
        controller.onLooking += OrderOutputLooking;
        controller.onDecreasingAdrenaline += OrderOutputDecreasingAdrenaline;

        targetCollisionDetecter = gameObject.transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        targetCollisionDetecter.onTriggerEnter += shooter.EnemyEnterTarget;
        targetCollisionDetecter.onTriggerExit += shooter.EnemyExitTarget;

        bodyCollisionDetecter = gameObject.transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        bodyCollisionDetecter.onTriggerEnter += OrderOutputDamaging;
        bodyCollisionDetecter.onTriggerEnter += smasher.PlayerEnetrCollider;
        bodyCollisionDetecter.onTriggerExit += smasher.PlayerExitCollider;
        bodyCollisionDetecter.onTriggerStay += OrderOutputAllowingSmash;
        bodyCollisionDetecter.onTriggerExit += OrderOutputNotAllowingSmash;

        meshCreator = gameObject.transform.Find("TargetCollider").GetComponent<MeshCreator>();
        meshCreator.CreateMeshCollider(parameter.Range, parameter.Reach);

        GameObject canvas = GameObject.Find("Canvas");
        playerUIs = canvas.GetComponentsInChildren<IPlayerUI>().ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerUIs?.ForEach(x => x.UpdateUI("Bullet", parameter.Parameter("Bullet")));
        playerUIs?.ForEach(x => x.UpdateUI("Life", parameter.Parameter("Life")));
        playerUIs?.ForEach(x => x.UpdateUI("Adrenaline", parameter.Parameter("Adrenaline")));
        playerUIs?.ForEach(x => x.UpdateUI("AdrenalineTank", parameter.Parameter("AdrenalineTank")));
    }

    private void Update()
    {
        
    }

    //�ړ��Ɋւ���e���\�b�h�����s
    private void OrderOutputMoving(Vector3 vector)
    {
        if (!stater.State["Movable"]) return;
        mover.Move(vector, parameter.Parameter("MoveSpeed"));
    }

    private void OrderOutputLooking(Vector3 vector)
    {
        if (!stater.State["Movable"]) return;
        mover.Look(vector);
    }

    private void OrderOutputFiring()
    {
        if (!stater.State["Shootable"]) return;
        if (!shooter.IsTarget) return;
        List<Collider> colliders = parameter.IsAdrenalinable ?
            shooter.Fire((int)parameter.Parameter("Bullet"), parameter.Parameter("Knockback"), parameter.Parameter("Attack"), parameter.GunEffect,
            parameter.Parameter("Critical")) :
            shooter.Fire((int)parameter.Parameter("Bullet"), parameter.Parameter("Knockback"), parameter.Parameter("Attack"), parameter.GunEffect);
        parameter.SetParameter("Bullet", -1);
        playerUIs?.ForEach(x => x.UpdateUI("Bullet", parameter.Parameter("Bullet")));
        if (colliders == null) return;
        smasher.MakeGroggy(colliders);
    }

    private void OrderOutputReloading()
    {
        if (!stater.State["Shootable"]) return;
        StartCoroutine(stater.WaitForStatusTransition("Shootable", parameter.Parameter("ReloadSpeed")));
        parameter.SetParameter("Bullet", parameter.Parameter("BulletMax") - parameter.Parameter("Bullet"));
        playerUIs?.ForEach(x => x.UpdateUI("Bullet", parameter.Parameter("Bullet")));
    }

    private void OrderOutputSmashing()
    {
        if (!stater.State["Smashable"]) return;
        stater.TransferState("Smashable", false);
        if (smasher.RemoveColliderInSmashers()) return;
        StartCoroutine(stater.WaitForStatusTransition("Damageable", parameter.SmashParam.SmashTime * 1.25f));
        StartCoroutine(stater.WaitForStatusTransition("Movable", parameter.SmashParam.SmashTime));
        smasher.Smash(parameter.SmashParam.SmashTime, parameter.SmashParam.Knockback, parameter.SmashParam.Attack, parameter.SmashParam.SmashEffect);
    }

    private void OrderOutputDamaging(Collider other)
    {
        if (!stater.State["Damageable"]) return;
        string key = damager.Damage(other, 2, out float damage);
        if (damage == 0) return;
        parameter.SetParameter(key, -damage);
        playerUIs?.ForEach(x => x.UpdateUI("Life", parameter.Parameter("Life")));
        StartCoroutine(stater.WaitForStatusTransition("Damageable", 2));
    }

    private void OrderOutputDecreasingAdrenaline()
    {
        parameter.SetParameter("Adrenaline", -parameter.Parameter("AdrenalineSpeed") * Time.deltaTime);
        playerUIs?.ForEach(x => x.UpdateUI("Adrenaline", parameter.Parameter("Adrenaline")));
    }

    private void OrderOutputIncreasingAdrenaline()
    {
        parameter.SetParameter("Adrenaline", 0.2f);
        playerUIs?.ForEach(x => x.UpdateUI("Adrenaline", parameter.Parameter("Adrenaline")));
        playerUIs?.ForEach(x => x.UpdateUI("AdrenalineTank", parameter.Parameter("AdrenalineTank")));
    }

    private void OrderOutputAllowingSmash(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        stater.TransferState("Smashable", true);
    }

    private void OrderOutputNotAllowingSmash(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        stater.TransferState("Smashable", false);
    }
}
