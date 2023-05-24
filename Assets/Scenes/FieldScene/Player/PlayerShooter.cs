using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerShooter : MonoBehaviour
{
    public bool IsTarget => targetingEnemies.Count > 0;

    //targetingEnemies�ɕߑ������G��Collider���i�[
    private List<Collider> targetingEnemies = new List<Collider>();

    public List<Collider> Fire(int bullet, float knockback, float attack, ParticleSystem gunEffect, float criticalPer = 0)
    {
        if (targetingEnemies.Count <= 0) return null;

        if (RemoveDestroyedEnemyInLockOn()) return null; //�j�����ꂽ�G���ߑ����X�g�ɂ����ꍇ�A���̃��\�b�h�Ń��X�g����폜
        if (bullet <= 0) return null; //�c�e���Ȃ��ꍇ�A�U���ł��Ȃ�
        List<Collider> enemies = HittingEnemy(targetingEnemies); //�U�������s
        List<Vector3> enemyVectors = enemies.Select(x => (x.transform.position - transform.position).normalized).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<ITargetable>().Hit(enemyVectors[i] * knockback, attack); //ITargetable��Hit���\�b�h�ɒl��n�����s
        }
        LookAt(enemies[0].transform); //�U�������G�̕�����U�����
        if (criticalPer != 0 && JudgeCritical(criticalPer)) enemies.ForEach(x => x.GetComponent<ITargetable>().Critical()); //�N���e�B�J������
        gunEffect.Play(); //�G�t�F�N�g���Đ�
        return enemies.Where(x => x.GetComponent<ITargetable>().IsGroggy == true).ToList(); //ITargetable��Groggy��true�ł���(���j���ꂽ)�R���_�[��Ԃ�
    }

    protected abstract List<Collider> HittingEnemy(List<Collider> targets);

    protected abstract void LookAt(Transform trans);

    private bool JudgeCritical(float criticalPer)
    {
        if (Random.Range(0, 1f) <= criticalPer) return true;
        return false;
    }

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
    private bool RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
        if (targetingEnemies.Count <= 0) return true;
        return false;
    }
}
