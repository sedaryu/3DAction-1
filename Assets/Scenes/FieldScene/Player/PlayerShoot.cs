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

    //�X�e�[�^�X
    private PlayerStatus status;

    //�ߑ������G�I�u�W�F�N�g���i�[
    List<EnemyController> lockOnEnemies = new List<EnemyController>();

    //�����[�h�����ǂ���
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

    //�G�I�u�W�F�N�g�̕ߑ�
    public void EnemyInCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Add(other.GetComponent<EnemyController>());
        }
    }

    //�G�I�u�W�F�N�g�̕ߑ�����
    public void EnemyOutCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Remove(other.GetComponent<EnemyController>());
        }
    }

    //���S�����G�����b�N�I�����珜�O
    private void RemoveDeadEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
