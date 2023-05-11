using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //エフェクター
    private MobEffecter effecter;
    //ターゲット
    private PlayerTarget target;
    //コントローラー
    private PlayerController controller;

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
        effecter = GetComponent<MobEffecter>();
        target = GetComponent<PlayerTarget>();
        controller = GetComponent<PlayerController>();
        targetCollisionDetecter = transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        bodyCollisionDetecter = transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        GameObject canvas = GameObject.Find("Canvas");
        bulletUIController = canvas.transform.Find("BulletUI").GetComponent<BulletUIController>();

        //移動
        playerMove = new PlayerMove(status);
        //射撃
        playerShoot = new PlayerShoot();
        playerShoot.AddTrigger("SetBullet", status.SetBullet);
        playerShoot.AddTrigger("UpdateBulletUI", bulletUIController.UpdateBulletUI);
        playerShoot.AddTrigger("GunEffectPlay", status.GunEffect.Play);
        targetCollisionDetecter.onTriggerEnter += playerShoot.EnemyInCollider;
        targetCollisionDetecter.onTriggerExit += playerShoot.EnemyOutCollider;
        //ダメージ
        playerDamage = new PlayerDamage(status);
        bodyCollisionDetecter.onTriggerEnter += playerDamage.EnemyAttackPlayer;
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
        playerMove.Move(controller.InputMoving());
        //ボタン入力を感知し、攻撃を実行
        playerShoot.Fire(controller.InputFiring());
        //ボタン入力を感知し、リロードを実行
        playerShoot.Reload(controller.InputReloading());
        //ボタン入力を感知し、スマッシュ攻撃を実行
        playerSmash.Smash(controller.InputSmashing());
    }
}
