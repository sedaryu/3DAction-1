using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    //パラメーター
    [SerializeField] protected MobParam param;
    //ステータス
    protected MobStatus status;
    //キャラクターの移動はNavMeshAgentで行う
    protected NavMeshAgent agent;
    //アニメーターを格納
    protected Animator animator;

    protected void Awake()
    {
        status = new MobStatus();
        agent = GetComponent<NavMeshAgent>(); //エージェントを取得
        animator = GetComponent<Animator>(); //アニメーターを取得
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
