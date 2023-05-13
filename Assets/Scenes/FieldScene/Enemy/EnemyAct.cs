using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour, ITargetable, IGrogable, IAttackable
{
    //�X�e�[�^�X
    private EnemyStatus status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //�m�b�N�o�b�J�[
    private EnemyKnockbacker knockbacker;

    //ITargetable
    public Transform Transform { get => transform; }
    //IAttackable
    public float Damage { get => status.EnemyParam.Attack; }

    //�A�N�g
    private EnemyMove enemyMove;
    private EnemyDamage enemyDamage;
    private EnemyGroggy enemyGroggy;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
        effecter = GetComponent<MobEffecter>();
        knockbacker = GetComponent<EnemyKnockbacker>();

        //�ړ�
        enemyMove = new EnemyMove(status);
        //�_���[�W
        enemyDamage = new EnemyDamage(status, effecter);
        //�O���b�L�[
        enemyGroggy = new EnemyGroggy(status, effecter);
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
        status.Damage(attack); //HitPoint�����������A�j���[�V�������Đ�
        effecter.InstanceEffect("Hit"); //�G�t�F�N�g�𐶐�
        int hit = knockbacker.JudgeObstacle(transform, status.Agent.radius, vector * status.EnemyParam.Weight);
        for (int i = 0; i < hit; i++)
        {
            status.Damage(attack * 2f); //Hit�����U���̓�{�̃_���[�W��ǉ��ŗ^����
            effecter.InstanceEffect("ObstacleHit"); //�G�t�F�N�g������������
        }
        knockbacker.Knockback(vector * status.EnemyParam.Weight); //�m�b�N�o�b�N
    }

    public void Grog(SmashAct smash)
    { 
        Instantiate(smash, transform);
    }

    public void Attack()
    { 
        //�A�j���[�V�������Đ�
    }
}
