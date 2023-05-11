using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //�X�e�[�^�X
    private PlayerStatus status;
    //�g�����X�t�H�[��
    private new PlayerTransform transform;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //�^�[�Q�b�g
    private PlayerTarget target;
    //�R���g���[���[
    private PlayerController controller;
    //���t�@�����V�b�h
    private PlayerReferenced referenced;

    //�A�N�g
    private PlayerMove playerMove;
    private PlayerShoot playerShoot;
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

        //�ړ�
        playerMove = new PlayerMove();
        playerMove.agentMove += transform.Agent.Move;
        playerMove.animatorSetFloat += status.Animator.SetFloat;
        playerMove.updateLookRotation += transform.UpdateRotation;
        //�ˌ�
        playerShoot = new PlayerShoot();
        playerShoot.hittingEnemy += status.GunParam.hittingEnemy;
        playerShoot.lookAt += transform.transform.LookAt;
        playerShoot.AddTrigger("SetBullet", status.SetBullet);
        playerShoot.AddTrigger("UpdateBulletUI", bulletUIController.UpdateBulletUI);
        playerShoot.AddTrigger("GunEffectPlay", status.GunEffectPlay);
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
        //�X�e�B�b�N���͂����m���A�ړ������s
        if (controller.InputMoving() != Vector3.zero)
        {
            playerMove.Move(status.IsNoMoveInvincible, controller.InputMoving(), status.PlayerParam.SpeedMax);
        }
        //�{�^�����͂����m���A�U�������s
        if (controller.InputFiring())
        {
            playerShoot.Fire(transform.transform.position, target.targetingEnemies, 
                             status.GunParam.Bullet, status.GunParam.Knockback, status.GunParam.Attack);
        }
        //�{�^�����͂����m���A�����[�h�����s
        if (controller.InputReloading())
        { 
            playerShoot.Reload(status.PlayerParam.ReloadSpeed, status.GunParam.BulletMax, status.GunParam.Bullet);
        }
            
        //�{�^�����͂����m���A�X�}�b�V���U�������s
        playerSmash.Smash(controller.InputSmashing());
    }
}
