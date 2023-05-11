using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //トランスフォーム
    private new PlayerTransform transform;
    //エフェクター
    private MobEffecter effecter;
    //ターゲット
    private PlayerTarget target;
    //コントローラー
    private PlayerController controller;
    //レファレンシッド
    private PlayerReferenced referenced;

    //アクト
    private PlayerMove playerMove;
    private PlayerShoot playerShoot;
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
        transform = GetComponent<PlayerTransform>();
        effecter = GetComponent<MobEffecter>();
        target = GetComponent<PlayerTarget>();
        controller = GetComponent<PlayerController>();
        referenced = GetComponent<PlayerReferenced>();

        targetCollisionDetecter = gameObject.transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        targetCollisionDetecter.onTriggerEnter += target.EnemyEnterTarget;
        targetCollisionDetecter.onTriggerExit += target.EnemyExitTarget;
        bodyCollisionDetecter = gameObject.transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        GameObject canvas = GameObject.Find("Canvas");
        bulletUIController = canvas.transform.Find("BulletUI").GetComponent<BulletUIController>();

        //移動
        playerMove = new PlayerMove();
        playerMove.agentMove += transform.Agent.Move;
        playerMove.animatorSetFloat += status.Animator.SetFloat;
        playerMove.updateLookRotation += transform.UpdateRotation;
        //射撃
        playerShoot = new PlayerShoot();
        playerShoot.hittingEnemy += status.GunParam.hittingEnemy;
        playerShoot.lookAt += transform.transform.LookAt;
        playerShoot.AddTrigger("SetBullet", status.SetBullet);
        playerShoot.AddTrigger("UpdateBulletUI", bulletUIController.UpdateBulletUI);
        playerShoot.AddTrigger("GunEffectPlay", status.GunEffectPlay);
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
        //スティック入力を感知し、移動を実行
        if (controller.InputMoving() != Vector3.zero)
        {
            playerMove.Move(status.IsNoMoveInvincible, controller.InputMoving(), status.PlayerParam.SpeedMax);
        }
        //ボタン入力を感知し、攻撃を実行
        if (controller.InputFiring())
        {
            playerShoot.Fire(transform.transform.position, target.targetingEnemies, 
                             status.GunParam.Bullet, status.GunParam.Knockback, status.GunParam.Attack);
        }
        //ボタン入力を感知し、リロードを実行
        if (controller.InputReloading())
        { 
            playerShoot.Reload(status.PlayerParam.ReloadSpeed, status.GunParam.BulletMax, status.GunParam.Bullet);
        }
            
        //ボタン入力を感知し、スマッシュ攻撃を実行
        playerSmash.Smash(controller.InputSmashing());
    }
}
