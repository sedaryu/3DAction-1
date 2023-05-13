using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooterPistol : PlayerShooter
{
    public override Transform HittingEnemy(Vector3 position, List<ITargetable> targets, float knockback, float attack)
    {
        List<float> distances = targets.Select(x => Vector3.Distance(position, x.Transform.position)).ToList(); //敵とプレイヤーの距離をリスト化
        int i = distances.FindIndex(x => x == distances.Min()); //その中で最も短い距離のインデックス番号を取得
        if (targets.Count - 1 < i || i == -1) return null;
        ITargetable target = targets[i];  //敵のスクリプトを取得
        Vector3 vector = (position - target.Transform.position).normalized * knockback; //ノックバック距離を設定
        target.Hit(vector, attack); //ITargetableのHitメソッドに値を渡し実行
        return target.Transform;
    }
}
