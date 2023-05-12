using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerShoot : MonoBehaviour
{
    //targetingEnemiesに捕捉した敵のEnemyReferencedを格納
    public List<EnemyReferenced> targetingEnemies = new List<EnemyReferenced>();

    public void Fire(int bullet, float knockback, float attack, ParticleSystem gunEffect)
    {
        if (targetingEnemies.Count <= 0) return;

        RemoveDestroyedEnemyInLockOn(); //破棄された敵が捕捉リストにいた場合、このメソッドでリストから削除
        if (bullet <= 0) return; //残弾がない場合、攻撃できない
        Transform enemy = HittingEnemy(transform.position, targetingEnemies, knockback, attack); //攻撃を実行
        transform.LookAt(enemy); //攻撃した敵の方向を振り向く
        gunEffect.Play(); //エフェクトを再生
    }

    public abstract Transform HittingEnemy(Vector3 position, List<EnemyReferenced> targets, float knockback, float attack);

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
