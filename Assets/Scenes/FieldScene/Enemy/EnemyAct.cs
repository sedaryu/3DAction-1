using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour, ITargetable, IGrogable, IAttackable
{
    //�p�����[�^�[
    private EnemyParameter parameter;
    //�X�e�[�^�[
    private EnemyStater stater;
    //�G�l�~�[���[�o�[
    private EnemyMover mover;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //�m�b�N�o�b�J�[
    private EnemyKnockbacker knockbacker;

    //IGrogable
    public bool Groggy { get => stater.State["Grogable"]; }
    //IAttackable
    //public float Damage { get => stater.State["Attackable"] ? parameter.EnemyParam.Attack : 0; }

    //�f�X�e�B�l�[�^�[
    private EnemyDestinater destinater;

    void Awake()
    {
        parameter = GetComponent<EnemyParameter>();
        stater = GetComponent<EnemyStater>();
        mover = GetComponent<EnemyMover>();
        effecter = GetComponent<MobEffecter>();
        knockbacker = GetComponent<EnemyKnockbacker>();

        destinater = GetComponent<EnemyDestinater>();
        destinater.onMoving += OrderOutputMoving;
    }

    private void OrderOutputMoving(Vector3 vector)
    {
        if (!stater.State["Movable"]) return;
        mover.Move(vector, parameter.EnemyParam.SpeedMax);
    }

    public void Hit(Vector3 vector, float attack)
    {
        if (stater.State["Grogable"]) return;

        parameter.Damage(attack); //HitPoint�����������A�j���[�V�������Đ�
        effecter.InstanceEffect("Hit"); //�G�t�F�N�g�𐶐�
        int hit = knockbacker.JudgeObstacle(transform, mover.Radius, vector * parameter.EnemyParam.Weight);
        for (int i = 0; i < hit; i++)
        {
            parameter.Damage(attack * 2f); //Hit�����U���̓�{�̃_���[�W��ǉ��ŗ^����
            effecter.InstanceEffect("ObstacleHit"); //�G�t�F�N�g������������
        }
        knockbacker.Knockback(vector * parameter.EnemyParam.Weight); //�m�b�N�o�b�N

        if (parameter.EnemyParam.HitPoint <= 0)
        {
            stater.TransferState("Grogable", true);
            stater.TransferState("Movable", false);
            stater.TransferState("Attackable", false);
        }
    }

    public void Grog(Smasher smash, float time)
    {
        if (stater.State["Smashable"]) return;

        Instantiate(smash, transform).transform.parent = transform;
        smash.StartTimer(time);
        stater.TransferState("Smashable", true);
    }

    public float Attack()
    {
        if (stater.State["Attackable"]) return 0;
        return parameter.EnemyParam.Attack;
    }
}
