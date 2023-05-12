using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //ムーバー
    private PlayerMover mover;
    //エフェクター
    private MobEffecter effecter;
    //ターゲット
    private PlayerShoot shoot;
    //コントローラー
    private IController controller;
    //レファレンシッド
    private PlayerReferenced referenced;

    //アクト
    private PlayerSt playerShoot;
    private PlayerDamage playerDamage;
    private PlayerSmash playerSmash;

    //コリダーイベント
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //UIイベント
    private BulletUIController bulletUIController;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        mover = GetComponent<PlayerMover>();
        effecter = GetComponent<MobEffecter>();
        shoot = GetComponent<PlayerShoot>();
        controller = GetComponent<IController>();
        referenced = GetComponent<PlayerReferenced>();

        targetCollisionDetecter = gameObject.transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        targetCollisionDetecter.onTriggerEnter += shoot.EnemyEnterTarget;
        targetCollisionDetecter.onTriggerExit += shoot.EnemyExitTarget;
        bodyCollisionDetecter = gameObject.transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        GameObject canvas = GameObject.Find("Canvas");
        bulletUIController = canvas.transform.Find("BulletUI").GetComponent<BulletUIController>();

        //ダメージ
        playerDamage = new PlayerDamage();
        playerDamage.isNormal += status.IsNormalMethod;
        playerDamage.AddTrigger("Damage", status.Damage);
        referenced.onTriggerAttacked += playerDamage.Damage;
        //スマッシュ
        playerSmash = new PlayerSmash(status, effecter);
        bodyCollisionDetecter.onTriggerEnter += playerSmash.PlayerInSmashRange;
        bodyCollisionDetecter.onTriggerExit += playerSmash.PlayerOutSmashRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletUIController.UpdateBulletUI(status.GunParam.Bullet.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //スティック入力を感知し、プレイヤーが移動可能な状態ならば、移動に関する各メソッドを実行
        if (controller.InputMoving() != Vector3.zero)
        {
            OrderOutputMoving();
        }
        //ボタン入力を感知し、攻撃を実行
        if (controller.InputFiring())
        {
            OrderOutputFiring();
        }
        //ボタン入力を感知し、リロードを実行
        if (controller.InputReloading())
        { 
            playerShoot.Reload(status.PlayerParam.ReloadSpeed, status.GunParam.BulletMax, status.GunParam.Bullet);
        }
            
        //ボタン入力を感知し、スマッシュ攻撃を実行
        playerSmash.Smash(controller.InputSmashing());
    }

    //プレイヤーが移動可能な状態ならば、移動に関する各メソッドを実行
    private void OrderOutputMoving()
    {
        if (status.IsNoMoveInvincible) return;

        mover.Move(controller.InputMoving(), status.PlayerParam.SpeedMax);
    }

    private void OrderOutputFiring()
    {
        shoot.Fire(status.GunParam.Bullet, status.GunParam.Knockback, status.GunParam.Attack, status.GunEffect);
        status.SetBullet(-1);
        //UI更新
    }
}
