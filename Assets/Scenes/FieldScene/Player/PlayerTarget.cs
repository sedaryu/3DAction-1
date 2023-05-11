using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    //targetingEnemiesに捕捉した敵のEnemyReferencedを格納
    public List<EnemyReferenced> targetingEnemies = new List<EnemyReferenced>();

    //敵の捕捉
    public void EnemyEnterTarget(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyAct>() == null) return;
            targetingEnemies.Add(other.GetComponent<EnemyReferenced>()); //敵のEnemyReferencedクラスを取得
        }
    }

    //敵の捕捉解除
    public void EnemyExitTarget(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetingEnemies.Remove(other.GetComponent<EnemyReferenced>()); //Colliderの範囲から外れた場合、リストから除外
        }
    }

    //破棄された敵をリストから除外
    private void RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
