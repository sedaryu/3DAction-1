using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    public void PistolHittingEnemy(Transform player, List<EnemyDamage> enemies, GunParam param)
    {
        List<float> distances = enemies.Select(x => Vector3.Distance(player.position, x.transform.position)).ToList();
        int i = distances.FindIndex(x => x == distances.Min());
        if (enemies.Count - 1 < i || i == -1) return;
        EnemyDamage enemy = enemies[i];
        player.LookAt(enemy.transform);
        Vector3 vector = player.forward.normalized * param.Knockback;
        enemy.Hit(vector, param.Attack);
    }
}
