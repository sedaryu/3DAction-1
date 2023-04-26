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
    [SerializeField] private GunParam _param;

    //�ߑ������G�I�u�W�F�N�g���i�[
    List<EnemyController> lockOnEnemies = new List<EnemyController>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && lockOnEnemies.Count > 0)
        {
            RemoveDeadEnemyInLockOn();
            Param.HittingEnemy.Invoke(this.transform, lockOnEnemies, Param);
        }
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
