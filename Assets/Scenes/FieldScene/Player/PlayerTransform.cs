using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTransform : MonoBehaviour
{
    //キャラクターの移動はNavMeshAgentで行う
    public NavMeshAgent Agent
    {
        get => _agent;
    }
    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>(); //エージェントを取得
    }

    public void UpdateRotation(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }
}
