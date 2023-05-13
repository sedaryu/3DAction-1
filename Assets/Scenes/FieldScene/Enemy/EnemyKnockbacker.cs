using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effecter;

public class EnemyKnockbacker : MonoBehaviour
{
    public void Knockback(Vector3 vector)
    {
        transform.Translate(vector, Space.World); //飛び道具の方向にノックバック
    }

    /// <summary>
    /// ノックバックする先にObstacleオブジェクトが存在するかRayを投射して判定し、
    /// 存在した場合、衝突ダメージを与える目的のメソッド
    /// </summary>
    /// <param name="vector">攻撃がヒットした際ノックバックする方向と距離</param>
    public int JudgeObstacle(Transform transform, float radius, Vector3 vector)
    {
        transform.rotation = Quaternion.LookRotation(-vector.normalized); //プレイヤーの方向を向く

        Vector3 avoidSpace = transform.forward * -0.1f * radius; //自分にRayが当たらないように隙間を作る
        //キャラの後方に、X軸に垂直なRayを半径五分割した間隔で投射
        Ray[] rays = new Ray[5]
        {
            new Ray(transform.position + transform.right * -radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.right * -0.5f * radius + transform.forward * -0.86f * radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.forward * -radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.right * 0.5f * radius + transform.forward * -0.86f * radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.right * radius + avoidSpace, vector.normalized),
        };

        int critical = 0;
        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, vector.magnitude - avoidSpace.magnitude, 1 << 8))
            {
                critical++;
            }
        }
        return critical;
    }
}
