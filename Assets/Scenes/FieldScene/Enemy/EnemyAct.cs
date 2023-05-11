using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{
    //�X�e�[�^�X
    private EnemyStatus status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;
    //���t�@�����V�b�h
    private EnemyReferenced referenced;

    //�A�N�g
    private EnemyMove enemyMove;
    private EnemyDamage enemyDamage;
    private EnemyGroggy enemyGroggy;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
        effecter = GetComponent<MobEffecter>();
        referenced = GetComponent<EnemyReferenced>();

        //�ړ�
        enemyMove = new EnemyMove(status);
        //�_���[�W
        enemyDamage = new EnemyDamage(status, effecter);
        //�O���b�L�[
        enemyGroggy = new EnemyGroggy(status, effecter);

        //�O�����甭������郁�\�b�h��ݒ�
        referenced.onTriggerAttacked += enemyDamage.Hit;
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
}
