using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MobController
{
    //�X�e�[�^�X
    private EnemyParameter status;
    //�ǐՂ���v���C���[
    private Transform player;

    void Awake()
    {
        status = GetComponent<EnemyParameter>();
        player = GameObject.Find("Player").GetComponent<Transform>(); //�v���C���[�̈ʒu���擾
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsNormal) status.Agent.destination = player.position; //�v���C���[��ǐ�
        status.Animator.SetFloat("MoveSpeed", status.Agent.velocity.magnitude); //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
    }
}
