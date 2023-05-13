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
    //シューター
    private PlayerShooter shooter;
    //コントローラー
    private IController controller;

    //アクト
    private PlayerSt playerShoot;
    private PlayerD playerDamage;
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
        shooter = GetComponent<PlayerShooter>();
        controller = GetComponent<IController>();

        targetCollisionDetecter = gameObject.transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        targetCollisionDetecter.onTriggerEnter += shooter.EnemyEnterTarget;
        targetCollisionDetecter.onTriggerExit += shooter.EnemyExitTarget;
        bodyCollisionDetecter = gameObject.transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        bodyCollisionDetecter.onTriggerEnter += OrderOutputDamaging;


        GameObject canvas = GameObject.Find("Canvas");
        bulletUIController = canvas.transform.Find("BulletUI").GetComponent<BulletUIController>();

        //ダメージ
        playerDamage = new PlayerD();
        playerDamage.AddTrigger("Damage", status.Damage);
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
        if (controller.InputMoving() != Vector3.zero && status.IsMobable)
        {
            OrderOutputMoving();
        }
        //ボタン入力を感知し、攻撃を実行
        if (controller.InputFiring() && status.IsShootable)
        {
            OrderOutputFiring();
        }
        //ボタン入力を感知し、リロードを実行
        if (controller.InputReloading() && status.IsShootable)
        {
            StartCoroutine(OrderOutputReloading());
        }
            
        //ボタン入力を感知し、スマッシュ攻撃を実行
        playerSmash.Smash(controller.InputSmashing());
    }

    //プレイヤーが移動可能な状態ならば、移動に関する各メソッドを実行
    private void OrderOutputMoving()
    {
        mover.Move(controller.InputMoving(), status.PlayerParam.SpeedMax);
    }

    private void OrderOutputFiring()
    {
        shooter.Fire(status.GunParam.Bullet, status.GunParam.Knockback, status.GunParam.Attack, status.GunEffect);
        status.SetBullet(-1);
        //UI更新
    }

    private IEnumerator OrderOutputReloading()
    {
        status.IsShootable = false;
        yield return new WaitForSeconds(status.PlayerParam.ReloadSpeed);
        status.SetBullet(status.GunParam.BulletMax - status.GunParam.Bullet);
        //UI更新
        status.isShootable = true;
    }

    private void OrderOutputDamaging(Collider other)
    {
        if (!status.IsDamageable) return;
        if (!other.TryGetComponent<IAttackable>(out IAttackable target)) return;
        status.isDamageable = false;
        StartCoroutine();
    }
}
