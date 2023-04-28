using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GunParam Param
    {
        get => _param;
    }
    private GunParam _param;

    [SerializeField] private GunParam initialParam;

    //ステータス
    private PlayerStatus status;

    //TargetingColliderで捕捉した敵オブジェクトを格納
    List<EnemyDamage> lockOnEnemies = new List<EnemyDamage>();

    //リロード中かどうか
    private bool reloading;

    void Awake()
    {
        _param = new GunParam(initialParam);
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //ボタン入力を感知し、攻撃を実行
        if (Input.GetButtonDown("Fire1") && lockOnEnemies.Count > 0) //TargetingColliderで捕捉した敵がいない場合は攻撃を行わない
        {
            RemoveDeadEnemyInLockOn(); //死亡した敵が捕捉リストにいた場合、このメソッドでリストから削除
            if (Param.Bullet <= 0) { Debug.Log("Empty"); return; } //残弾がない場合、攻撃できない
            Param.HittingEnemy.Invoke(this.transform, lockOnEnemies, Param); //攻撃を実行
            _param.Bullet--; //弾薬を消費
        }

        //ボタン入力を感知し、リロードを実行
        if (Input.GetButtonDown("Reload") && !reloading) //リロード中は行えない
        {
            reloading = true;
            StartCoroutine(Reloading()); //リロード完了待機時間を開始
        }
    }

    private IEnumerator Reloading()
    { 
        yield return new WaitForSeconds(status.Param.ReloadSpeed); //リロード完了までの待機時間はプレイヤーのパラメーターに依存
        Param.Bullet += Param.BulletMax - Param.Bullet; //足りない分だけ装填される
        reloading = false;
        Debug.Log("Reloaded");
    }

    //敵オブジェクトの捕捉
    public void EnemyInCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
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

    //死亡した敵をリストから除外
    private void RemoveDeadEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
