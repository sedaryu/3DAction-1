using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    public void PistolHittingEnemy(Transform player, List<EnemyController> enemies, GunParam param)
    {
        List<float> distances = enemies.Select(x => Vector3.Distance(player.position, x.transform.position)).ToList();
        EnemyController enemy = enemies[distances.FindIndex(x => x == distances.Min())];
        player.LookAt(enemy.transform);
        Vector3 vector = player.forward.normalized * param.Knockback;
        enemy.Hit(vector, param.Attack);
    }
}
