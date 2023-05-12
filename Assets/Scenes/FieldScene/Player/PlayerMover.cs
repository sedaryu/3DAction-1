using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour, IMover
{
    //キャラクターの移動はNavMeshAgentで行う
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); //エージェントを取得
    }

    public float Move(Vector3 vector, float speed)
    {
        vector *= speed * Time.deltaTime;
        agent.Move(vector);
        transform.rotation = Quaternion.LookRotation(vector);
        return vector.magnitude;
    }
}
