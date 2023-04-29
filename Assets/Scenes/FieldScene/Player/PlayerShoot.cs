using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //コントローラー
    private PlayerController controller;

    //TargetingColliderで捕捉した敵オブジェクトを格納
    List<EnemyDamage> lockOnEnemies = new List<EnemyDamage>();

    //リロード中かどうか
    private bool reloading;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //ボタン入力を感知し、攻撃を実行
        Fire(controller.InputFiring());
        //ボタン入力を感知し、リロードを実行
        Reload(controller.InputReloading());
    }

    private void Fire(bool input)
    {
        if (!input) return;
        if (lockOnEnemies.Count <= 0) return;

        RemoveDestroyedEnemyInLockOn(); //破棄された敵が捕捉リストにいた場合、このメソッドでリストから削除
        if (status.GunParam.Bullet <= 0) { Debug.Log("Empty"); return; } //残弾がない場合、攻撃できない
        status.GunParam.HittingEnemy.Invoke(this.transform, lockOnEnemies, status.GunParam); //攻撃を実行
        status.SetBullet(-1); //弾薬を消費
    }

    private void Reload(bool input)
    {
        if (!input) return;
        if (reloading) return;

        reloading = true;
        StartCoroutine(ReloadTime()); //リロード完了待機時間を開始
    }

    private IEnumerator ReloadTime()
    { 
        yield return new WaitForSeconds(status.PlayerParam.ReloadSpeed); //リロード完了までの待機時間はプレイヤーのパラメーターに依存
        status.SetBullet(status.GunParam.BulletMax - status.GunParam.Bullet); //足りない分だけ装填される
        reloading = false;
    }

    //敵オブジェクトの捕捉
    public void EnemyInCollider(Collider other)
    {
        Debug.Log("InCollider");
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyDamage>() == null) return;
            lockOnEnemies.Add(other.GetComponent<EnemyDamage>()); //敵の被ダメージを制御するクラスを取得
        }
    }

    //敵オブジェクトの捕捉解除
    public void EnemyOutCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Remove(other.GetComponent<EnemyDamage>()); //TargetingColliderの範囲から外れた場合、リストから除外
        }
    }

    //破棄された敵をリストから除外
    private void RemoveDestroyedEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
