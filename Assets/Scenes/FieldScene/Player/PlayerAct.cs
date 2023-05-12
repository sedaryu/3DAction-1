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
    //�^�[�Q�b�g
    private PlayerShoot shoot;
    //�R���g���[���[
    private IController controller;
    //���t�@�����V�b�h
    private PlayerReferenced referenced;

    //�A�N�g
    private PlayerSt playerShoot;
    private PlayerDamage playerDamage;
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
        shoot = GetComponent<PlayerShoot>();
        controller = GetComponent<IController>();
        referenced = GetComponent<PlayerReferenced>();

        targetCollisionDetecter = gameObject.transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        targetCollisionDetecter.onTriggerEnter += shoot.EnemyEnterTarget;
        targetCollisionDetecter.onTriggerExit += shoot.EnemyExitTarget;
        bodyCollisionDetecter = gameObject.transform.Find("BodyCollider").GetComponent<CollisionDetecter>();
        GameObject canvas = GameObject.Find("Canvas");
        bulletUIController = canvas.transform.Find("BulletUI").GetComponent<BulletUIController>();

        //�_���[�W
        playerDamage = new PlayerDamage();
        playerDamage.isNormal += status.IsNormalMethod;
        playerDamage.AddTrigger("Damage", status.Damage);
        referenced.onTriggerAttacked += playerDamage.Damage;
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
        if (controller.InputMoving() != Vector3.zero)
        {
            OrderOutputMoving();
        }
        //�{�^�����͂����m���A�U�������s
        if (controller.InputFiring())
        {
            OrderOutputFiring();
        }
        //�{�^�����͂����m���A�����[�h�����s
        if (controller.InputReloading())
        { 
            playerShoot.Reload(status.PlayerParam.ReloadSpeed, status.GunParam.BulletMax, status.GunParam.Bullet);
        }
            
        //�{�^�����͂����m���A�X�}�b�V���U�������s
        playerSmash.Smash(controller.InputSmashing());
    }

    //�v���C���[���ړ��\�ȏ�ԂȂ�΁A�ړ��Ɋւ���e���\�b�h�����s
    private void OrderOutputMoving()
    {
        if (status.IsNoMoveInvincible) return;

        mover.Move(controller.InputMoving(), status.PlayerParam.SpeedMax);
    }

    private void OrderOutputFiring()
    {
        shoot.Fire(status.GunParam.Bullet, status.GunParam.Knockback, status.GunParam.Attack, status.GunEffect);
        status.SetBullet(-1);
        //UI�X�V
    }
}
