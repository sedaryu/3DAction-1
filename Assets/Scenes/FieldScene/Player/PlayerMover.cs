using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour, IMover
{
    //�L�����N�^�[�̈ړ���NavMeshAgent�ōs��
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); //�G�[�W�F���g���擾
    }

    public float Move(Vector3 vector, float speed)
    {
        vector *= speed * Time.deltaTime;
        agent.Move(vector);
        transform.rotation = Quaternion.LookRotation(vector);
        return vector.magnitude;
    }
}
