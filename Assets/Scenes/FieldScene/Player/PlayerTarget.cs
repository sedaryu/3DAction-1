using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    //targetingEnemies�ɕߑ������G��EnemyReferenced���i�[
    public List<EnemyReferenced> targetingEnemies = new List<EnemyReferenced>();

    //�G�̕ߑ�
    public void EnemyEnterTarget(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyAct>() == null) return;
            targetingEnemies.Add(other.GetComponent<EnemyReferenced>()); //�G��EnemyReferenced�N���X���擾
        }
    }

    //�G�̕ߑ�����
    public void EnemyExitTarget(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetingEnemies.Remove(other.GetComponent<EnemyReferenced>()); //Collider�͈̔͂���O�ꂽ�ꍇ�A���X�g���珜�O
        }
    }

    //�j�����ꂽ�G�����X�g���珜�O
    private void RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
