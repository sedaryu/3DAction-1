using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MobController
{
    //パラメーター
    private EnemyParam param;
    //追跡するプレイヤー
    private Transform player;
    //HitPoint表示UI
    private GameObject hitPointCircle;

    // Start is called before the first frame update
    void Start()
    {
        param = (EnemyParam)_param;
        agent.speed = Random.Range(0.5f, 4.0f); //param.Speed; //パラメーターからスピードを取得
        player = GameObject.Find("Player").GetComponent<Transform>(); //プレイヤーの位置を取得
        hitPointCircle = transform.Find("HitPointCircle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsMovable) agent.destination = player.position; //プレイヤーを追跡
        animator.SetFloat("MoveSpeed", agent.velocity.magnitude); //アニメーターに移動スピードを反映
        //if (JudgeGrounded()) agent.destination = player.position; //落下撃破ありの場合
    }

    public void Hit(Vector3 vector, float attack)
    {
        status.GoToDamageStateIfPossible(); //キャラの状態をDamageに遷移
        animator.SetTrigger("Damage");
        transform.rotation = Quaternion.LookRotation(-vector.normalized);
        StartCoroutine(FreezeMotion(vector, attack));
    }

    private IEnumerator FreezeMotion(Vector3 vector, float attack)
    {
        hitPointCircle.transform.Translate(vector * param.Weight * 0.25f, Space.World);
        yield return new WaitForSeconds(0.03f);
        hitPointCircle.transform.Translate(vector * param.Weight, Space.World);
        yield return new WaitForSeconds(0.03f);
        hitPointCircle.transform.localPosition = new Vector3(0, 0.001f, 0);
        JudgeCollapsed(vector);
        Knockback(vector);
        Damage(attack);
        status.GoToNormalStateIfPossible(); //キャラの状態をNormalに遷移
    }

    //ノックバック処理
    public void Knockback(Vector3 vector)
    {
        transform.Translate(vector * param.Weight, Space.World); //飛び道具の方向にノックバック
    }

    //ダメージ処理
    protected override void Damage(float attack)
    {
        param.HitPoint -= attack;
    }

    private void JudgeCollapsed(Vector3 vector)
    {
        Vector3 avoidSpace = transform.forward * -0.1f * agent.radius;
        Ray[] rays = new Ray[5]
        {
            new Ray(this.transform.position + transform.right * -agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * -0.5f * agent.radius + transform.forward * -0.86f * agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.forward * -agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * 0.5f * agent.radius + transform.forward * -0.86f * agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * agent.radius + avoidSpace, vector.normalized),
        };

        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector * param.Weight, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, (vector * param.Weight).magnitude - avoidSpace.magnitude, 1 << 7 | 1 << 8))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    Debug.Log($"ObstacleCollapsel!!! : {hit.transform.gameObject.name}");
                }

                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log($"EnemyCollapse!!! : {hit.transform.gameObject.name}");
                }

                break;
            }
        }
    }

    protected bool JudgeGrounded() //接地判定処理を行う
    {
        Ray[] rays = new Ray[4]
        { 
            new Ray(this.transform.position - new Vector3(transform.localScale.x * 0.5f, 0, 0), Vector3.down),
            new Ray(this.transform.position + new Vector3(transform.localScale.x * 0.5f, 0, 0), Vector3.down),
            new Ray(this.transform.position - new Vector3(0, 0, transform.localScale.z * 0.5f), Vector3.down),
            new Ray(this.transform.position + new Vector3(0, 0, transform.localScale.z * 0.5f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, Vector3.down * 0.3f, Color.red);
            if (!Physics.Raycast(rays[i], 0.3f, 1 << 6))
            {
                FallFromPlane();
                return false;
            }
        }
        return true;
    }

    //落下処理
    private void FallFromPlane()
    {
        agent.enabled = false; //NavMeshAgentを解除
        //Rigidbodyを有効化
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        StartCoroutine(FallingDeath()); //消滅処理を実行
    }

    //落下後一定時間後消滅
    private IEnumerator FallingDeath()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
