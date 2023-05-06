using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <Summary>
/// このスクリプトをアタッチしたオブジェクトと、
/// MainCameraの間に存在する障害物を透過させる目的のクラス
/// </Summary>
public class TransObstacle : MonoBehaviour
{
    //レイを飛ばす先
    private Transform target;
    //前フレームレイがHitしたObstacle
    private MakeTransparent obstacle;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastToTarget();
    }

    /// <summary>
    /// レイを投射し、ヒットしたObstacleレイヤーかつMakeTransparentスクリプトがアタッチされたオブジェクトを
    /// 透過させる目的のメソッド
    /// </summary>
    private void RaycastToTarget()
    {
        if (target == null) return;
        Vector3 directionToTarget = target.position - transform.position; //ターゲット方向へのベクトルを取得
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, directionToTarget.magnitude, 1 << 8))
        {
            obstacle = hit.transform.gameObject.GetComponent<MakeTransparent>();
            if (obstacle == null) return;
            obstacle.Transparent();
        }
        else
        {
            //障害物の透過を解除する場合
            if (obstacle != null)
            {
                obstacle.CancelTransparent();
                obstacle = null;
            }
        }
    }
}
