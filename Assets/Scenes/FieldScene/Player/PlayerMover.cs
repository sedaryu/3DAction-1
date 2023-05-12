using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    //�L�����N�^�[�̈ړ���NavMeshAgent�ōs��
    private NavMeshAgent agent;

    private Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); //�G�[�W�F���g���擾
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 vector, float speed)
    {
        agent.Move(vector * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(vector);

        animator.SetFloat("MoveSpeed", speed * Time.deltaTime);
    }
}
