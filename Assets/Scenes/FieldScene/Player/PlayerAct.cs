using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAct : MonoBehaviour
{
    //�X�e�[�^�X
    private PlayerStatus status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //�R���g���[���[
    private PlayerController controller;

    //�A�N�g
    private PlayerMove playerMove;
    private PlayerShoot playerShoot;
    private PlayerDamage playerDamage;
    private PlayerSmash playerSmash;

    //�R���_�[�C�x���g
    private CollisionDetecter targetCollisionDetecter;
    private CollisionDetecter bodyCollisionDetecter;
    //UI�C�x���g
    public Action<string> onFiring;
    public Action<string> onReloading;
    public Action<string> onSmashing;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        effecter = GetComponent<MobEffecter>();
        controller = GetComponent<PlayerController>();
        targetCollisionDetecter = transform.Find("TargetCollider").GetComponent<CollisionDetecter>();
        bodyCollisionDetecter = transform.Find("BodyCollider").GetComponent<CollisionDetecter>();

        //�ړ�
        playerMove = new PlayerMove(status);
        //�ˌ�
        playerShoot = new PlayerShoot(status);
        playerShoot.AddTrigger("OnFiring", onFiring);
        playerShoot.AddTrigger("OnReloading", onReloading);
        targetCollisionDetecter.onTriggerEnter += playerShoot.EnemyInCollider;
        targetCollisionDetecter.onTriggerExit += playerShoot.EnemyOutCollider;
        //�_���[�W
        playerDamage = new PlayerDamage(status);
        bodyCollisionDetecter.onTriggerEnter += playerDamage.EnemyAttackPlayer;
        //�X�}�b�V��
        playerSmash = new PlayerSmash(status, effecter);
        bodyCollisionDetecter.onTriggerEnter += playerSmash.PlayerInSmashRange;
        bodyCollisionDetecter.onTriggerExit += playerSmash.PlayerOutSmashRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        onFiring?.Invoke(status.GunParam.Bullet.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //�X�e�B�b�N���͂����m���A�ړ������s
        playerMove.Move(controller.InputMoving());
        //�{�^�����͂����m���A�U�������s
        playerShoot.Fire(controller.InputFiring());
        //�{�^�����͂����m���A�����[�h�����s
        playerShoot.Reload(controller.InputReloading());
        //�{�^�����͂����m���A�X�}�b�V���U�������s
        playerSmash.Smash(controller.InputSmashing());
    }
}
