using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooterPistol : PlayerShooter
{
    protected override List<Collider> HittingEnemy(List<Collider> targets)
    {
        List<float> distances = targets.Select(x => Vector3.Distance(transform.position, x.transform.position)).ToList(); //敵とプレイヤーの距離をリスト化
        int i = distances.FindIndex(x => x == distances.Min()); //その中で最も短い距離のインデックス番号を取得
        if (targets.Count - 1 < i || i == -1) return null;
        List<Collider> targetList = new List<Collider>();
        targetList.Add(targets[i]);  //敵のColliderを取得
        return targetList;
    }

    protected override void LookAt(Transform trans)
    {
        transform.LookAt(trans);
    }
}
