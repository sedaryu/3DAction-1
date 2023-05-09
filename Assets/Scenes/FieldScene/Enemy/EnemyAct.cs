using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{
    //�X�e�[�^�X
    private EnemyStatus status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;

    //�A�N�g
    private EnemyMove enemyMove;
    private EnemyDamage enemyDamage;
    private EnemyGroggy enemyGroggy;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
        effecter = GetComponent<MobEffecter>();

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

    public void Hit(Vector3 vector, float attack, SmashParam smash)
    {
        if (enemyDamage.Hit(vector, attack)) enemyGroggy.Groggy(smash);
    }
}
