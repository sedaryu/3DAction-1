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

    //捕捉した敵オブジェクトを格納
    List<EnemyController> lockOnEnemies = new List<EnemyController>();

    //リロード中かどうか
    private bool reloading;

    void Awake()
    {
        _param = new GunParam(initialParam);
        status = GetComponent<PlayerStatus>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Param.Bullet);

        if (Input.GetButtonDown("Fire1") && lockOnEnemies.Count > 0)
        {
            RemoveDeadEnemyInLockOn();
            if (Param.Bullet <= 0) { Debug.Log("Empty"); return; }
            Param.HittingEnemy.Invoke(this.transform, lockOnEnemies, Param);
            _param.Bullet--;
        }

        if (Input.GetButtonDown("Reload") && !reloading)
        {
            reloading = true;
            StartCoroutine(Reloading());
        }
    }

    private IEnumerator Reloading()
    { 
        yield return new WaitForSeconds(status.Param.ReloadSpeed);
        Param.Bullet += Param.BulletMax - Param.Bullet;
        reloading = false;
        Debug.Log("Reloaded");
    }

    //敵オブジェクトの捕捉
    public void EnemyInCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Add(other.GetComponent<EnemyController>());
        }
    }

    //敵オブジェクトの捕捉解除
    public void EnemyOutCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Remove(other.GetComponent<EnemyController>());
        }
    }

    //死亡した敵をロックオンから除外
    private void RemoveDeadEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
