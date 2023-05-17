using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerShooter : MonoBehaviour
{
    //targetingEnemies�ɕߑ������G��Collider���i�[
    private List<Collider> targetingEnemies = new List<Collider>();

    public List<Collider> Fire(int bullet, float knockback, float attack, ParticleSystem gunEffect)
    {
        if (targetingEnemies.Count <= 0) return null;

        RemoveDestroyedEnemyInLockOn(); //�j�����ꂽ�G���ߑ����X�g�ɂ����ꍇ�A���̃��\�b�h�Ń��X�g����폜
        if (bullet <= 0) return null; //�c�e���Ȃ��ꍇ�A�U���ł��Ȃ�
        List<Collider> enemies = HittingEnemy(targetingEnemies); //�U�������s
        List<Vector3> enemyVectors = enemies.Select(x => (x.transform.position - transform.position).normalized).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<ITargetable>().Hit(enemyVectors[i] * knockback, attack); //ITargetable��Hit���\�b�h�ɒl��n�����s
        }
        LookAt(enemies[0].transform); //�U�������G�̕�����U�����
        gunEffect.Play(); //�G�t�F�N�g���Đ�
        return enemies.Where(x => x.TryGetComponent<IGrogable>(out IGrogable grog) == true && grog.Groggy == true).ToList();
        
    }

    public abstract List<Collider> HittingEnemy(List<Collider> targets);

    public abstract void LookAt(Transform trans);

    //�G�̕ߑ�
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(other); //�G��Collider���擾
    }

    //�G�̕ߑ�����
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(other); //Collider�͈̔͂���O�ꂽ�ꍇ�A���X�g���珜�O
    }

    //�j�����ꂽ�G�����X�g���珜�O
    private void RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
