using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
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
    //�M���U���[
    private PlayerGatherer gatherer;

    //�R���g���[���[
    private Controller controller;
    //�R���_�[
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //�^�[�Q�b�g
    private MeshCreator meshCreator;
    //UI
    private List<IPlayerUI> playerUIs;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        parameter = GetComponent<PlayerParameter>();
        stater = GetComponent<PlayerStater>();
        mover = GetComponent<PlayerMover>();
        effecter = GetComponent<MobEffecter>();
        shooter = GetComponent<PlayerShooter>();
        damager = GetComponent<PlayerDamager>();
        smasher = GetComponent<PlayerSmasher>();
        smasher.SetSmash(parameter.Smash);
        smasher.onKilling += OrderOutputIncreasingAdrenaline;
        gatherer = GetComponent<PlayerGatherer>();

        controller = GetComponent<Controller>();
        controller.onMoving += OrderOutputMoving;
        controller.onFiring += OrderOutputFiring;
        controller.onReloading += OrderOutputReloading;
        controller.onSmashing += OrderOutputSmashing;
        controller.onLooking += OrderOutputLooking;
        controller.onBursting += OrderOutputBursting;
        controller.onDecreasingAdrenaline += OrderOutputDecreasingAdrenaline;

        targetCollisionDetecter = gameObject.transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        targetCollisionDetecter.onTriggerEnter += shooter.EnemyEnterTarget;
        targetCollisionDetecter.onTriggerExit += shooter.EnemyExitTarget;

        bodyCollisionDetecter = gameObject.transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        bodyCollisionDetecter.onTriggerEnter += OrderOutputDamaging;
        bodyCollisionDetecter.onTriggerEnter += OrderOutputGathering;
        bodyCollisionDetecter.onTriggerEnter += OrderOutputReleasing;

        bodyCollisionDetecter.onTriggerEnter += smasher.PlayerEnetrCollider;
        bodyCollisionDetecter.onTriggerExit += smasher.PlayerExitCollider;
        bodyCollisionDetecter.onTriggerStay += OrderOutputAllowingSmash;
        bodyCollisionDetecter.onTriggerExit += OrderOutputNotAllowingSmash;

        meshCreator = gameObject.transform.Find("TargetCollider").GetComponent<MeshCreator>();
        meshCreator.CreateMeshCollider(parameter.Range, parameter.Reach);

        GameObject canvas = GameObject.Find("Canvas");
        playerUIs = canvas.GetComponentsInChildren<IPlayerUI>().ToList();

        playerUIs?.ForEach(x => x.UpdateUI("Bullet", parameter.Parameter("Bullet")));
        playerUIs?.ForEach(x => x.UpdateUI("Life", parameter.Parameter("Life")));
        playerUIs?.ForEach(x => x.UpdateUI("Adrenaline", parameter.Parameter("Adrenaline")));
        playerUIs?.ForEach(x => x.UpdateUI("AdrenalineTank", parameter.Parameter("AdrenalineTank")));
        playerUIs?.ForEach(x => x.UpdateUI("Items", gatherer.ListUpItems()));
    }

    private void Update()
    {

    }

    //�ړ��Ɋւ���e���\�b�h�����s
    private void OrderOutputMoving(Vector3 vector)
    {
        if (!stater.State["Movable"]) return;
        mover.Move(vector, parameter.IsAdrenalinable ? parameter.Parameter("MoveSpeedMax") : parameter.Parameter("MoveSpeed"));
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
        parameter.ChangeParameter("Bullet", -1);
        playerUIs?.ForEach(x => x.UpdateUI("Bullet", parameter.Parameter("Bullet")));
        if (colliders == null) return;
        smasher.MakeGroggy(colliders);
    }

    private void OrderOutputReloading()
    {
        if (!stater.State["Shootable"]) return;
        StartCoroutine(stater.WaitForStatusTransition("Shootable", parameter.Parameter("ReloadSpeed")));
        parameter.ChangeParameter("Bullet", parameter.Parameter("BulletMax") - parameter.Parameter("Bullet"));
        playerUIs?.ForEach(x => x.UpdateUI("Bullet", parameter.Parameter("Bullet")));
    }

    private async void OrderOutputSmashing()
    {
        if (!stater.State["Smashable"]) return;
        if (smasher.IsSmashing) return;
        if (smasher.RemoveColliderInSmashers()) return;
        stater.TransferState("Damageable", false);
        stater.TransferState("Movable", false);
        //StartCoroutine(stater.WaitForStatusTransition("Damageable", parameter.SmashTime * 1.25f));
        //StartCoroutine(stater.WaitForStatusTransition("Movable", parameter.SmashTime));
        Task smash = await smasher.Smash();
        StartCoroutine(stater.WaitForStatusTransition("Damageable", parameter.SmashTime * 0.25f));
        stater.TransferState("Movable", true);
    }

    private void OrderOutputDamaging(Collider other)
    {
        if (!stater.State["Damageable"]) return;
        string key = damager.Damage(other, 2, out float damage);
        if (damage == 0) return;
        parameter.ChangeParameter(key, -damage);
        playerUIs?.ForEach(x => x.UpdateUI(key, parameter.Parameter(key)));
        if (key == "Life") StartCoroutine(stater.WaitForStatusTransition("Damageable", 2));
    }

    private void OrderOutputGathering(Collider other)
    {
        float weight = gatherer.Gather(other);
        if(weight == 0) return;
        parameter.ChangeParameter("MoveSpeed", -weight);
        playerUIs?.ForEach(x => x.UpdateUI("Items", gatherer.ListUpItems()));
    }

    private void OrderOutputReleasing(Collider other)
    {
        if (gatherer.Release(other))
        {
            parameter.RevertParameter("MoveSpeed");
            parameter.ChangeParameter("Life", 1);
        }
        playerUIs?.ForEach(x => x.UpdateUI("Life", parameter.Parameter("Life")));
        playerUIs?.ForEach(x => x.UpdateUI("Items", gatherer.ListUpItems()));
    }

    private void OrderOutputBursting()
    {
        if (parameter.Parameter("AdrenalineTank") <= 0) return;
        parameter.ChangeParameter("AdrenalineTank", -1);
        parameter.SetParameter("Adrenaline", 0.999f);
        playerUIs?.ForEach(x => x.UpdateUI("Adrenaline", parameter.Parameter("Adrenaline")));
        playerUIs?.ForEach(x => x.UpdateUI("AdrenalineTank", parameter.Parameter("AdrenalineTank")));
    }

    private void OrderOutputDecreasingAdrenaline()
    {
        parameter.ChangeParameter("Adrenaline", -parameter.Parameter("AdrenalineSpeed") * Time.deltaTime);
        playerUIs?.ForEach(x => x.UpdateUI("Adrenaline", parameter.Parameter("Adrenaline")));
    }

    private void OrderOutputIncreasingAdrenaline()
    {
        parameter.ChangeParameter("Adrenaline", 0.1f);
        playerUIs?.ForEach(x => x.UpdateUI("Adrenaline", parameter.Parameter("Adrenaline")));
        playerUIs?.ForEach(x => x.UpdateUI("AdrenalineTank", parameter.Parameter("AdrenalineTank")));
    }

    private void OrderOutputAllowingSmash(Collider other)
    {
        if (!other.TryGetComponent<Smash>(out Smash smash)) return;
        stater.TransferState("Smashable", true);
    }

    private void OrderOutputNotAllowingSmash(Collider other)
    {
        if (!other.TryGetComponent<Smash>(out Smash smash)) return;
        stater.TransferState("Smashable", false);
    }
}
