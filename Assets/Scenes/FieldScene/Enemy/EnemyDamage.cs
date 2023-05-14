using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵キャラクターに攻撃がヒットした際の、ダメージまたノックバック処理、
/// 同時に衝突判定を行う目的のクラス
/// </summary>
public class EnemyDamage
{
    //ステータス
    private EnemyParameter status;
    //エフェクター
    private MobEffecter effecter;

    public EnemyDamage(EnemyParameter _status, MobEffecter _effecter)
    { 
        status = _status;
        effecter = _effecter;
    }

    /// <summary>
    /// プレイヤーの攻撃がヒットした際の、ノックバック、ダメージ、衝突判定、アニメーション、エフェクトを処理
    /// 攻撃の結果HitPointが0以下になった場合は、戻り値としてtrueを返す、それ以外はfalseを返す
    /// </summary>
    /// <param name="vector">攻撃がヒットした際のノックバック方向と距離</param>
    /// <param name="attack">攻撃がヒットした際受けるダメージ量</param>
    /// <returns>攻撃の結果HitPointが0以下になった場合、trueを返す、それ以外はfalse</returns>
    public void Hit(Vector3 vector, float attack)
    {
        status.transform.rotation = Quaternion.LookRotation(-vector.normalized); //プレイヤーの方向を向く

        status.Animator.SetTrigger("Damage"); //被ダメージの際のアニメーションを実行
        JudgeObstacle(vector, attack); //衝突判定を実行
        effecter.InstanceEffect("Hit"); //エフェクトを生成
        Knockback(vector); //ノックバックを実行
        status.Damage(attack); //HitPointを減少させる
    }

    /// <summary>
    /// 引数のvectorにWeightのパラメーターを掛けたぶんノックバックさせる
    /// </summary>
    /// <param name="vector">攻撃がヒットした際ノックバックする方向と距離</param>
    public void Knockback(Vector3 vector)
    {
        status.transform.Translate(vector * status.EnemyParam.Weight, Space.World); //飛び道具の方向にノックバック
    }

    /// <summary>
    /// ノックバックする先にObstacleオブジェクトが存在するかRayを投射して判定し、
    /// 存在した場合、衝突ダメージを与える目的のメソッド
    /// </summary>
    /// <param name="vector">攻撃がヒットした際ノックバックする方向と距離</param>
    /// <param name="attack">ヒットした攻撃のダメージ</param>
    private void JudgeObstacle(Vector3 vector, float attack)
    {
        Vector3 avoidSpace = status.transform.forward * -0.1f * status.Agent.radius; //自分にRayが当たらないように隙間を作る
        //キャラの後方に、X軸に垂直なRayを半径五分割した間隔で投射
        Ray[] rays = new Ray[5]
        {
            new Ray(status.transform.position + status.transform.right * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.right * -0.5f * status.Agent.radius + status.transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.forward * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.right * 0.5f * status.Agent.radius + status.transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.right * status.Agent.radius + avoidSpace, vector.normalized),
        };

        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector * status.EnemyParam.Weight, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, (vector * status.EnemyParam.Weight).magnitude - avoidSpace.magnitude, 1 << 8))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    status.Damage(attack * 2f); //Hitした攻撃の二倍のダメージを追加で与える
                    effecter.InstanceEffect("ObstacleHit"); //エフェクトも発生させる
                }
            }
        }
    }
}
