using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    //キャラクターの移動はNavMeshAgentで行う
    private NavMeshAgent agent;

    private Animator animator;

    public float Radius { get => agent.radius; }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); //エージェントを取得
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 vector, float speed)
    {
        animator.SetFloat("MoveSpeed", speed * Time.deltaTime);

        agent.destination = vector;
        agent.speed = speed;
    }

    public void DisableAgent()
    {
        agent.enabled = false;
    }
}
