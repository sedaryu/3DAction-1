using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //ステータス
    private EnemyStatus status;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
    }

    /// <summary>
    /// プレイヤーの攻撃がヒットした際の、ノックバック、ダメージ、衝突判定、アニメーション、エフェクトを処理
    /// </summary>
    /// <param name="vector">攻撃がヒットした際のノックバック方向と距離</param>
    /// <param name="attack">攻撃がヒットした際受けるダメージ量</param>
    /// <param name="smash">攻撃の結果HitPointが0になった際、出現させるSmashColliderのパラメーターを格納</param>
    public void Hit(Vector3 vector, float attack, SmashParam smash)
    {
        transform.rotation = Quaternion.LookRotation(-vector.normalized); //プレイヤーの方向を向く

        status.Animator.SetTrigger("Damage"); //被ダメージの際のアニメーションを実行
        JudgeObstacle(vector, attack); //衝突判定を実行
        Instantiate(status.Effecter.GetEffectFromKey("Hit"), transform.position, transform.rotation); //エフェクトを生成
        Knockback(vector); //ノックバックを実行
        if (!status.IsSmashable && status.Damage(attack)) //攻撃の結果HitPointが0になった際の処理
        {
            status.GoToDieStateIfPossible(); //0以下ならば状態がDieに移行
            GameObject smashCollider = Instantiate(smash.SmashCollider, transform); //SmashColliderを生成
            smashCollider.transform.parent = transform; //子オブジェクト化
            smashCollider.GetComponent<Smash>().StartTimer(smash.DestroyTime);
        }
    }

    //ノックバック処理
    public void Knockback(Vector3 vector)
    {
        transform.Translate(vector * status.Param.Weight, Space.World); //飛び道具の方向にノックバック
    }

    private void JudgeObstacle(Vector3 vector, float attack)
    {
        Vector3 avoidSpace = transform.forward * -0.1f * status.Agent.radius;
        Ray[] rays = new Ray[5]
        {
            new Ray(this.transform.position + transform.right * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * -0.5f * status.Agent.radius + transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.forward * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * 0.5f * status.Agent.radius + transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * status.Agent.radius + avoidSpace, vector.normalized),
        };

        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector * status.Param.Weight, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, (vector * status.Param.Weight).magnitude - avoidSpace.magnitude, 1 << 8))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    status.Damage(attack * 2f);
                    Instantiate(status.Effecter.GetEffectFromKey("ObstacleHit"), transform);
                }
            }
        }
    }
}
