using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class PlayerShoot : Act
{
    //ステータス
    private PlayerStatus status;

    //lockOnEnemiesに捕捉した敵オブジェクトを格納
    List<EnemyAct> lockOnEnemies = new List<EnemyAct>();

    //リロード中かどうか
    private bool reloading;

    public PlayerShoot(PlayerStatus _status)
    {
        status = _status;
    }

    public void Fire(bool input)
    {
        if (!input) return;
        if (lockOnEnemies.Count <= 0) return;

        RemoveDestroyedEnemyInLockOn(); //破棄された敵が捕捉リストにいた場合、このメソッドでリストから削除
        if (status.GunParam.Bullet <= 0) return; //残弾がない場合、攻撃できない
        status.GunParam.HittingEnemy.Invoke(status.transform, lockOnEnemies, status.GunParam, status.SmashParam); //攻撃を実行
        status.SetBullet(-1); //弾薬を消費
        OnTrigger("OnFiring", status.GunParam.Bullet.ToString()); //UIを更新
        status.GunEffect.Play(); //エフェクトを再生
    }

    public void Reload(bool input)
    {
        if (!input) return;
        if (reloading) return;

        Task _ = ReloadTime(); //リロードを開始
    }

    private async Task ReloadTime()
    {
        reloading = true;
        await Task.Delay(TimeSpan.FromSeconds(status.PlayerParam.ReloadSpeed)); //リロード完了までの待機時間はプレイヤーのパラメーターに依存
        status.SetBullet(status.GunParam.BulletMax - status.GunParam.Bullet); //足りない分だけ装填される
        OnTrigger("OnReloading", status.GunParam.Bullet.ToString()); //UIを更新
        reloading = false;
    }

    //敵オブジェクトの捕捉
    public void EnemyInCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyAct>() == null) return;
            lockOnEnemies.Add(other.GetComponent<EnemyAct>()); //敵の被ダメージを制御するクラスを取得
        }
    }

    //敵オブジェクトの捕捉解除
    public void EnemyOutCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Remove(other.GetComponent<EnemyAct>()); //TargetingColliderの範囲から外れた場合、リストから除外
        }
    }

    //破棄された敵をリストから除外
    private void RemoveDestroyedEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
