using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MobController
{
    //パラメーター
    private EnemyParam param;
    //追跡するプレイヤー
    private Transform player;

    //圧迫判定用
    private Vector3 presentPosition;
    private float knockbackMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        param = (EnemyParam)_param;
        player = GameObject.Find("Player").GetComponent<Transform>(); //プレイヤーの位置を取得
        agent.speed = 0;// Random.Range(0.5f, 2.0f); //param.Speed; //パラメーターからスピードを取得
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsMovable) agent.destination = player.position; //プレイヤーを追跡
        //if (JudgeGrounded()) agent.destination = player.position; //落下撃破ありの場合
    }

    //攻撃ヒット時の処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile")) //飛び道具が当たった場合
        {
            ProjectileParam projectileParam = other.GetComponent<ProjectileController>().Param;
            Knockback(other.transform.forward.normalized * param.Weight * projectileParam.Knockback); //ノックバックを実行
            Damage(projectileParam.Attack); //ダメージ処理を実行
            Destroy(other.gameObject); //飛び道具を消滅
            status.GoToDamageStateIfPossible(); //キャラの状態をDamageに遷移
        }
    }

    //壁めり込み時の処理
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle") && status.IsDamageble) //障害物にめり込んだ場合
        {
            Debug.Log("Damage!!");
        }
    }

    public void Hit(Vector3 vector, float attack)
    {
        presentPosition = transform.position;
        knockbackMagnitude = Knockback(vector);
        Damage(attack);
        StartCoroutine(FrameOfDamageState());
        //Debug.Log($"enemyKB: {(transform.position - presentPosition).magnitude + 0.001} KB: {knockbackMagnitude}");
    }

    //ノックバック処理
    public float Knockback(Vector3 vector)
    {
        Vector3 knockback = vector * param.Weight;
        transform.Translate(knockback, Space.World); //飛び道具の方向にノックバック
        return knockback.magnitude;
    }

    //ダメージ処理
    protected override void Damage(float attack)
    {
        param.HitPoint -= attack;
    }

    private IEnumerator FrameOfDamageState()
    {
        status.GoToDamageStateIfPossible(); //キャラの状態をDamageに遷移
        yield return null;
        JudgeCollapsed();
        status.GoToNormalStateIfPossible();
    }

    private void JudgeCollapsed()
    {
        //Debug.Log($"enemyKB: {(transform.position - presentPosition).magnitude + 0.1f} KB: {knockbackMagnitude}");
        //圧迫判定
        if ((transform.position - presentPosition).magnitude + 0.1f < knockbackMagnitude)
        {
            Debug.Log("Critical!!!");
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
