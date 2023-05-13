using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //�X�e�[�^�X
    private PlayerStatus status;
    //���[�o�[
    private PlayerMover mover;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //�V���[�^�[
    private PlayerShooter shooter;
    //�R���g���[���[
    private IController controller;

    //�A�N�g
    private PlayerSt playerShoot;
    private PlayerD playerDamage;
    private PlayerSmash playerSmash;

    //�R���_�[�C�x���g
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //UI�C�x���g
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

        //�_���[�W
        playerDamage = new PlayerD();
        playerDamage.AddTrigger("Damage", status.Damage);
        //�X�}�b�V��
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
        //�X�e�B�b�N���͂����m���A�v���C���[���ړ��\�ȏ�ԂȂ�΁A�ړ��Ɋւ���e���\�b�h�����s
        if (controller.InputMoving() != Vector3.zero && status.IsMobable)
        {
            OrderOutputMoving();
        }
        //�{�^�����͂����m���A�U�������s
        if (controller.InputFiring() && status.IsShootable)
        {
            OrderOutputFiring();
        }
        //�{�^�����͂����m���A�����[�h�����s
        if (controller.InputReloading() && status.IsShootable)
        {
            StartCoroutine(OrderOutputReloading());
        }
            
        //�{�^�����͂����m���A�X�}�b�V���U�������s
        playerSmash.Smash(controller.InputSmashing());
    }

    //�v���C���[���ړ��\�ȏ�ԂȂ�΁A�ړ��Ɋւ���e���\�b�h�����s
    private void OrderOutputMoving()
    {
        mover.Move(controller.InputMoving(), status.PlayerParam.SpeedMax);
    }

    private void OrderOutputFiring()
    {
        shooter.Fire(status.GunParam.Bullet, status.GunParam.Knockback, status.GunParam.Attack, status.GunEffect);
        status.SetBullet(-1);
        //UI�X�V
    }

    private IEnumerator OrderOutputReloading()
    {
        status.IsShootable = false;
        yield return new WaitForSeconds(status.PlayerParam.ReloadSpeed);
        status.SetBullet(status.GunParam.BulletMax - status.GunParam.Bullet);
        //UI�X�V
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
