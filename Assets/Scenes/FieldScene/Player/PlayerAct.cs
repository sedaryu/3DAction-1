using System;
using System.Collections;
using System.Collections.Generic;
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
    //アドレナライナー
    private PlayerAdrenaliner adrenaliner;

    //コントローラー
    private Controller controller;
    //コリダー
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //ターゲット
    private MeshCreator meshCreator;
    //UI
    private List<IBulletUI> bulletUIs;
    private List<ILifeUI> lifeUIs;
    private List<IAdrenalineUI> adrenalineUIs;

    void Awake()
    {
        parameter = GetComponent<PlayerParameter>();
        stater = GetComponent<PlayerStater>();
        mover = GetComponent<PlayerMover>();
        effecter = GetComponent<MobEffecter>();
        shooter = GetComponent<PlayerShooter>();
        damager = GetComponent<PlayerDamager>();
        smasher = GetComponent<PlayerSmasher>();
        adrenaliner = GetComponent<PlayerAdrenaliner>();
        smasher.SetSmasher(parameter.SmashParam.SmashCollider, parameter.SmashParam.DestroyTime);
        smasher.onKilling += OrderOutputIncreasingAdrenaline;

        controller = GetComponent<Controller>();
        controller.onMoving += OrderOutputMoving;
        controller.onFiring += OrderOutputFiring;
        controller.onReloading += OrderOutputReloading;
        controller.onSmashing += OrderOutputSmashing;
        controller.onLooking += OrderOutputLooking;

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
        bulletUIs = canvas.GetComponentsInChildren<IBulletUI>().ToList();
        lifeUIs = canvas.GetComponentsInChildren<ILifeUI>().ToList();
        adrenalineUIs = canvas.GetComponentsInChildren<IAdrenalineUI>().ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletUIs?.ForEach(x => x.UpdateMagazinTextUI(parameter.Parameter("Bullet").ToString()));
        lifeUIs?.ForEach(x => x.UpdateLifeTextUI(parameter.Parameter("Life").ToString()));
        adrenalineUIs?.ForEach(x => x.UpdateAdrenalineUI(parameter.Parameter("Adrenaline")));
        adrenalineUIs?.ForEach(x => x.UpdateAdrenalineTankUI((int)parameter.Parameter("AdrenalineTank")));
    }

    private void Update()
    {
        adrenaliner.DecreaseAdrenaline();
        adrenalineUIs?.ForEach(x => x.UpdateAdrenalineUI(adrenaliner.Adrenaline));
    }

    //移動に関する各メソッドを実行
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
        List<Collider> colliders = shooter.Fire((int)parameter.Parameter("Bullet"), parameter.Parameter("Knockback"),
                                                parameter.Parameter("Attack"), parameter.GunEffect);
        parameter.SetParameter("Bullet", -1);
        bulletUIs?.ForEach(x => x.UpdateMagazinTextUI(((int)parameter.Parameter("Bullet")).ToString()));
        if (colliders == null) return;
        smasher.MakeGroggy(colliders);
    }

    private void OrderOutputReloading()
    {
        if (!stater.State["Shootable"]) return;
        StartCoroutine(stater.WaitForStatusTransition("Shootable", parameter.Parameter("ReloadSpeed")));
        parameter.SetParameter("Bullet", parameter.Parameter("BulletMax") - parameter.Parameter("Bullet"));
        bulletUIs?.ForEach(x => x.UpdateMagazinTextUI(((int)parameter.Parameter("Bullet")).ToString()));
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
        float damage = damager.Damage(other, 2);
        if (damage == 0) return;
        parameter.Damage(damage);
        lifeUIs?.ForEach(x => x.UpdateLifeTextUI(((int)parameter.Parameter("Life")).ToString()));
        StartCoroutine(stater.WaitForStatusTransition("Damageable", 2));
    }

    private void OrderOutputIncreasingAdrenaline()
    {
        adrenaliner.IncreaseAdrenaline();
        adrenalineUIs?.ForEach(x => x.UpdateAdrenalineUI(adrenaliner.Adrenaline));
        adrenalineUIs?.ForEach(x => x.UpdateAdrenalineTankUI(adrenaliner.AdrenalineTank));
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
