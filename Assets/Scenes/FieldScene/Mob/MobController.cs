using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MobController : MonoBehaviour
{
    //キャラクターの移動はNavMeshAgentで行う
    protected NavMeshAgent agent;
    //アニメーターを格納
    private Animator animator;

    protected void Awake()
    {
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
        agent.Move(vector * 5.5f * Time.deltaTime); //移動入力を更新

        //キャラクターの向きを更新
        if (vector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000 * Time.deltaTime);
        }
        //アニメーターに移動スピードを反映
        animator.SetFloat("MoveSpeed" , vector.magnitude);
    }

    protected bool JudgeGrounded() //接地判定処理を行う
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        Debug.DrawRay(this.transform.position, Vector3.down * 0.1f, Color.red);

        if (Physics.Raycast(ray, 0.1f, 1 << 6))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
