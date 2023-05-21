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

    //コントローラー
    private Controller controller;
    //コリダー
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //UI
    private List<IBulletUI> bulletUIs;
    private List<ILifeUI> lifeUIs;

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
        bodyCollisionDetecter.onTriggerStay += OrderOutputAllowingSmash;
        bodyCollisionDetecter.onTriggerExit += OrderOutputNotAllowingSmash;
        bodyCollisionDetecter.onTriggerExit += smasher.PlayerExitCollider;

        GameObject canvas = GameObject.Find("Canvas");
        bulletUIs = canvas.GetComponentsInChildren<IBulletUI>().ToList();
        lifeUIs = canvas.GetComponentsInChildren<ILifeUI>().ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletUIs.ForEach(x => x.UpdateMagazinTextUI(parameter.GunParam.Bullet.ToString()));
        lifeUIs.ForEach(x => x.UpdateLifeTextUI(parameter.PlayerParam.HitPoint.ToString()));
    }

    //移動に関する各メソッドを実行
    private void OrderOutputMoving(Vector3 vector)
    {
        if (!stater.State["Movable"]) return;
        mover.Move(vector, parameter.PlayerParam.SpeedMax);
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
        List<Collider> colliders = shooter.Fire(parameter.GunParam.Bullet, parameter.GunParam.Knockback, 
                                                parameter.GunParam.Attack, parameter.GunEffect);
        parameter.SetBullet(-1);
        bulletUIs.ForEach(x => x.UpdateMagazinTextUI(parameter.GunParam.Bullet.ToString()));
        if (colliders == null) return;
        smasher.MakeGroggy(colliders);
    }

    private void OrderOutputReloading()
    {
        if (!stater.State["Shootable"]) return;
        StartCoroutine(stater.WaitForStatusTransition("Shootable", parameter.PlayerParam.ReloadSpeed));
        parameter.SetBullet(parameter.GunParam.BulletMax - parameter.GunParam.Bullet);
        bulletUIs.ForEach(x => x.UpdateMagazinTextUI(parameter.GunParam.Bullet.ToString()));
    }

    private void OrderOutputSmashing()
    {
        if (!stater.State["Smashable"]) return;
        stater.TransferState("Smashable", false);
        if (smasher.RemoveColliderInSmashers()) return;
        StartCoroutine(stater.WaitForStatusTransition("Damageable", parameter.SmashParam.SmashTime));
        StartCoroutine(stater.WaitForStatusTransition("Movable", parameter.SmashParam.SmashTime));
        smasher.Smash(parameter.SmashParam.SmashTime, parameter.SmashParam.Knockback, parameter.SmashParam.Attack, parameter.SmashParam.SmashEffect);
    }

    private void OrderOutputDamaging(Collider other)
    {
        if (!stater.State["Damageable"]) return;
        float damage = damager.Damage(other, 2);
        if (damage == 0) return;
        parameter.Damage(damage);
        lifeUIs.ForEach(x => x.UpdateLifeTextUI(parameter.PlayerParam.HitPoint.ToString()));
        StartCoroutine(stater.WaitForStatusTransition("Damageable", 2));
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
