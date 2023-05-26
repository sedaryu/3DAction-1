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

    private void Update()
    {
    }

    public void Move(Vector3 vector, float speed)
    {
        animator.SetFloat("MoveSpeed", (vector * speed * Time.deltaTime).magnitude);

        if (vector == Vector3.zero) return;
        agent.Move(vector * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(vector);
    }

    public void Look(Vector3 vector)
    {
        if (vector == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(vector);
    }
}
