using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerShooter : MonoBehaviour
{
    public bool IsTarget => targetingEnemies.Count > 0;

    //targetingEnemiesに捕捉した敵のColliderを格納
    private List<Collider> targetingEnemies = new List<Collider>();

    public List<Collider> Fire(int bullet, float knockback, float attack, ParticleSystem gunEffect)
    {
        if (targetingEnemies.Count <= 0) return null;

        if (RemoveDestroyedEnemyInLockOn()) return null; //破棄された敵が捕捉リストにいた場合、このメソッドでリストから削除
        if (bullet <= 0) return null; //残弾がない場合、攻撃できない
        List<Collider> enemies = HittingEnemy(targetingEnemies); //攻撃を実行
        List<Vector3> enemyVectors = enemies.Select(x => (x.transform.position - transform.position).normalized).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<ITargetable>().Hit(enemyVectors[i] * knockback, attack); //ITargetableのHitメソッドに値を渡し実行
        }
        LookAt(enemies[0].transform); //攻撃した敵の方向を振り向く
        gunEffect.Play(); //エフェクトを再生
        return enemies.Where(x => x.GetComponent<ITargetable>().IsGroggy == true).ToList(); //ITargetableのGroggyがtrueである(撃破された)コリダーを返す

    }

    public abstract List<Collider> HittingEnemy(List<Collider> targets);

    public abstract void LookAt(Transform trans);

    //敵の捕捉
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(other); //敵のColliderを取得
    }

    //敵の捕捉解除
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(other); //Colliderの範囲から外れた場合、リストから除外
    }

    //破棄された敵をリストから除外
    private bool RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
        if (targetingEnemies.Count <= 0) return true;
        return false;
    }
}
