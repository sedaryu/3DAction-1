using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerShooter : MonoBehaviour
{
    //targetingEnemies�ɕߑ������G��ITargetable���i�[
    public List<ITargetable> targetingEnemies = new List<ITargetable>();

    public void Fire(int bullet, float knockback, float attack, ParticleSystem gunEffect)
    {
        if (targetingEnemies.Count <= 0) return;

        RemoveDestroyedEnemyInLockOn(); //�j�����ꂽ�G���ߑ����X�g�ɂ����ꍇ�A���̃��\�b�h�Ń��X�g����폜
        if (bullet <= 0) return; //�c�e���Ȃ��ꍇ�A�U���ł��Ȃ�
        Transform enemy = HittingEnemy(transform.position, targetingEnemies, knockback, attack); //�U�������s
        transform.LookAt(enemy); //�U�������G�̕�����U�����
        gunEffect.Play(); //�G�t�F�N�g���Đ�
    }

    public abstract Transform HittingEnemy(Vector3 position, List<ITargetable> targets, float knockback, float attack);

    //�G�̕ߑ�
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(target); //�G��ITargetable�N���X���擾
    }

    //�G�̕ߑ�����
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(target); //Collider�͈̔͂���O�ꂽ�ꍇ�A���X�g���珜�O
    }

    //�j�����ꂽ�G�����X�g���珜�O
    private void RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
