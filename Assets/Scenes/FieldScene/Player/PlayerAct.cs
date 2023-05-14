using System;
using System.Collections;
using System.Collections.Generic;
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
    private BulletUIController bulletUIController;

    void Awake()
    {
        parameter = GetComponent<PlayerParameter>();
        stater = GetComponent<PlayerStater>();
        mover = GetComponent<PlayerMover>();
        effecter = GetComponent<MobEffecter>();
        shooter = GetComponent<PlayerShooter>();
        damager = GetComponent<PlayerDamager>();
        smasher = GetComponent<PlayerSmasher>();

        controller = GetComponent<Controller>();
        controller.onMoving += OrderOutputMoving;
        controller.onFiring += OrderOutputFiring;
        controller.onReloading += OrderOutputReloading;
        //controller.onSmashing += OrderOutputSmashing;

        targetCollisionDetecter = gameObject.transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        targetCollisionDetecter.onTriggerEnter += shooter.EnemyEnterTarget;
        targetCollisionDetecter.onTriggerExit += shooter.EnemyExitTarget;
        bodyCollisionDetecter = gameObject.transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        bodyCollisionDetecter.onTriggerEnter += OrderOutputDamaging;


        GameObject canvas = GameObject.Find("Canvas");
        bulletUIController = canvas.transform.Find("BulletUI").GetComponent<BulletUIController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletUIController.UpdateBulletUI(parameter.GunParam.Bullet.ToString());
    }

    //移動に関する各メソッドを実行
    private void OrderOutputMoving(Vector3 vector)
    {
        if (!stater.State["Movable"]) return;
        mover.Move(vector, parameter.PlayerParam.SpeedMax);
    }

    private void OrderOutputFiring()
    {
        if (!stater.State["Shootable"]) return;
        List<Collider> colliders = shooter.Fire(parameter.GunParam.Bullet, parameter.GunParam.Knockback, 
                                                parameter.GunParam.Attack, parameter.GunEffect);
        parameter.SetBullet(-1);
        //UI更新
        if (colliders == null) return;
        smasher.MakeGroggy(colliders, parameter.SmashParam.SmashCollider);
    }

    private void OrderOutputReloading()
    {
        if (!stater.State["Shootable"]) return;
        StartCoroutine(stater.WaitForStatusTransition("Shootable", parameter.PlayerParam.ReloadSpeed));
        parameter.SetBullet(parameter.GunParam.BulletMax - parameter.GunParam.Bullet);
        //UI更新
    }

    private void OrderOutputDamaging(Collider other)
    {
        if (!stater.State["Damageable"]) return;
        StartCoroutine(stater.WaitForStatusTransition("Damageable", 2));
        parameter.Damage(damager.Damage(other));
        StartCoroutine(damager.WaitForRendererColorTransition(2));
    }
}
