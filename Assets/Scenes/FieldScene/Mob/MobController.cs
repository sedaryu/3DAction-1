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

    //キャラクターの移動を管理するメソッド
    protected void Move(Vector3 vector)
    {
        if (!status.IsMovable) return;

        agent.Move(vector * param.Speed * Time.deltaTime); //移動入力を更新

        //キャラクターの向きを更新
        if (vector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2000 * Time.deltaTime);
        }
        //アニメーターに移動スピードを反映
        animator.SetFloat("MoveSpeed" , vector.magnitude);
    }
}
