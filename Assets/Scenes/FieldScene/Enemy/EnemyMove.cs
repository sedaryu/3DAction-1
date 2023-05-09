using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyMove 
{
    //�X�e�[�^�X
    private EnemyStatus status;
    //�ǐՂ���v���C���[
    private Transform player;

    public EnemyMove(EnemyStatus _status)
    {
        status = _status;
        player = GameObject.Find("Player").GetComponent<Transform>(); //�v���C���[�̈ʒu���擾
    }

    public void Move()
    {
        if (status.IsNormal && player != null) status.Agent.destination = player.position; //�v���C���[��ǐ�
        else status.Agent.speed = 0;

        status.Animator.SetFloat("MoveSpeed", status.Agent.velocity.magnitude); //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
    }
}
