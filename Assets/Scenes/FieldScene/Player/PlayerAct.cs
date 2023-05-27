using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //パラメーター
    private PlayerParameter parameter;
    //ステーター
    private PlayerStater stater;
    //ムーバー
    private PlayerMover mover;
    //エフェクター
    private MobEffecter effecter;
    //シューター
    private PlayerShooter shooter;
    //ダメージャー
    private PlayerDamager damager;
    //スマッシャー
    private PlayerSmasher smasher;
    //ギャザラー
    private PlayerGatherer gatherer;

    //コントローラー
    private Controller controller;
    //コリダー
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //ターゲット
    private MeshCreator meshCreator;
    //UI
    private List<IPlayerUI> playerUIs;

    void Awake()
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

    //移動に関する各メソッドを実行
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

    private void OrderOutputSmashing()
    {
        if (!stater.State["Smashable"]) return;
        if (smasher.IsSmashing) return;
        //stater.TransferState("Smashable", false);
        if (smasher.RemoveColliderInSmashers()) return;
        StartCoroutine(stater.WaitForStatusTransition("Damageable", parameter.SmashTime * 1.25f));
        StartCoroutine(stater.WaitForStatusTransition("Movable", parameter.SmashTime));
        smasher.Smash();
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
        parameter.ChangeParameter("MoveSpeed", -weight);
    }

    private void OrderOutputReleasing(Collider other)
    { 
        gatherer.Release(other);
        parameter.RevertParameter("MoveSpeed");
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
