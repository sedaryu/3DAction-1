using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour, ITargetable, IGrogable, IAttackable
{
    //�p�����[�^�[
    private EnemyParameter parameter;
    //�X�e�[�^�[
    private EnemyStater stater;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //�m�b�N�o�b�J�[
    private EnemyKnockbacker knockbacker;

    //IGrogable
    public bool Groggy { get => stater.State["Grogable"]; }
    //IAttackable
    public float Damage { get => parameter.EnemyParam.Attack; }

    //�A�N�g
    private EnemyMove enemyMove;
    private EnemyDamage enemyDamage;
    private EnemyGroggy enemyGroggy;

    void Awake()
    {
        parameter = GetComponent<EnemyParameter>();
        stater = GetComponent<EnemyStater>();
        effecter = GetComponent<MobEffecter>();
        knockbacker = GetComponent<EnemyKnockbacker>();

        //�ړ�
        enemyMove = new EnemyMove(parameter);
        //�_���[�W
        enemyDamage = new EnemyDamage(parameter, effecter);
        //�O���b�L�[
        enemyGroggy = new EnemyGroggy(parameter, effecter);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ����ăv���C���[��ǐՂ���
        enemyMove.Move();
    }

    public void Hit(Vector3 vector, float attack)
    {
        if (stater.State["Grogable"]) return;

        parameter.Damage(attack); //HitPoint�����������A�j���[�V�������Đ�
        effecter.InstanceEffect("Hit"); //�G�t�F�N�g�𐶐�
        int hit = knockbacker.JudgeObstacle(transform, parameter.Agent.radius, vector * parameter.EnemyParam.Weight);
        for (int i = 0; i < hit; i++)
        {
            parameter.Damage(attack * 2f); //Hit�����U���̓�{�̃_���[�W��ǉ��ŗ^����
            effecter.InstanceEffect("ObstacleHit"); //�G�t�F�N�g������������
        }
        knockbacker.Knockback(vector * parameter.EnemyParam.Weight); //�m�b�N�o�b�N
        if (parameter.EnemyParam.HitPoint <= 0) stater.TransferState("Grogable", true);
    }

    public void Grog(SmashAct smash)
    {
        if (stater.State["Smashable"]) return;

        Instantiate(smash, transform).transform.parent = transform;
        stater.TransferState("Smashable", true);
    }

    public void Attack()
    { 
        //�A�j���[�V�������Đ�
    }
}
