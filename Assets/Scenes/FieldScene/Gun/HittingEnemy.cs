using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    public void PistolHittingEnemy(Transform player, List<EnemyAct> enemies, GunParam gun, SmashParam smash)
    {
        List<float> distances = enemies.Select(x => Vector3.Distance(player.position, x.transform.position)).ToList(); //敵とプレイヤーの距離をリスト化
        int i = distances.FindIndex(x => x == distances.Min()); //その中で最も短い距離のインデックス番号を取得
        if (enemies.Count - 1 < i || i == -1) return;
        EnemyAct enemy = enemies[i];  //敵のスクリプトを取得
        player.LookAt(enemy.transform); //敵の方向をプレイヤーが向く
        Vector3 vector = player.forward.normalized * gun.Knockback; //ノックバック距離を設定
        enemy.Hit(vector, gun.Attack, smash); //敵スクリプトのダメージメソッドに値を渡し実行
    }
}
