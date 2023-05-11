using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTransform : MonoBehaviour
{
    //�L�����N�^�[�̈ړ���NavMeshAgent�ōs��
    public NavMeshAgent Agent
    {
        get => _agent;
    }
    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>(); //�G�[�W�F���g���擾
    }

    public void UpdateRotation(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }
}
