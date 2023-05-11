using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    public Transform PistolHittingEnemy(Vector3 position, List<EnemyReferenced> enemies, float knockback, float attack)
    {
        List<float> distances = enemies.Select(x => Vector3.Distance(position, x.transform.position)).ToList(); //敵とプレイヤーの距離をリスト化
        int i = distances.FindIndex(x => x == distances.Min()); //その中で最も短い距離のインデックス番号を取得
        if (enemies.Count - 1 < i || i == -1) return null;
        EnemyReferenced enemy = enemies[i];  //敵のスクリプトを取得
        //Vector3 vector = player.forward.normalized * knockback; //ノックバック距離を設定
        Vector3 vector = (position - enemy.transform.position).normalized * knockback; //ノックバック距離を設定
        enemy.OnAttacked(vector, attack); //敵スクリプトのダメージメソッドに値を渡し実行
        return enemy.transform;
    }
}
