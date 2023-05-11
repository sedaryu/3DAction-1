using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class PlayerShoot : Act
{
    //敵への攻撃処理(銃ごとに変わる)
    public event Func<Vector3, List<EnemyReferenced>, float, float, Transform> hittingEnemy;
    //攻撃時のプレイヤーの振り向き
    public event Action<Transform> lookAt;

    //リロード中かどうか
    private bool reloading;

    public void Fire(Vector3 position, List<EnemyReferenced> enemies, int bullet, float knockback, float attack)
    {
        if (enemies.Count <= 0) return;

        //RemoveDestroyedEnemyInLockOn(); //破棄された敵が捕捉リストにいた場合、このメソッドでリストから削除
        if (bullet <= 0) return; //残弾がない場合、攻撃できない
        Transform enemy = hittingEnemy.Invoke(position, enemies, knockback, attack); //攻撃を実行
        lookAt.Invoke(enemy); //攻撃した敵の方向を振り向く
        OnTrigger("SetBullet", -1); //弾薬を消費
        OnTrigger("UpdateBulletUI", (bullet - 1).ToString()); //UIを更新
        OnTrigger("GunEffectPlay"); //エフェクトを再生
    }

    public void Reload(float reloadSpeed, int bulletMax, int bullet)
    {
        if (reloading) return;

        Task _ = ReloadTime(reloadSpeed, bulletMax, bullet); //リロードを開始
    }

    private async Task ReloadTime(float reloadSpeed, int bulletMax, int bullet)
    {
        reloading = true;
        await Task.Delay(TimeSpan.FromSeconds(reloadSpeed)); //リロード完了までの待機時間はプレイヤーのパラメーターに依存
        OnTrigger("SetBullet", bulletMax - bullet); //足りない分だけ装填される
        OnTrigger("UpdateBulletUI", bulletMax.ToString()); //UIを更新
        reloading = false;
    }
}
