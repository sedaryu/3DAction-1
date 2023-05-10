using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class PlayerShoot : Act
{
    //�X�e�[�^�X
    private PlayerStatus status;

    //lockOnEnemies�ɕߑ������G�I�u�W�F�N�g���i�[
    List<EnemyAct> lockOnEnemies = new List<EnemyAct>();

    //�����[�h�����ǂ���
    private bool reloading;

    public PlayerShoot(PlayerStatus _status)
    {
        status = _status;
    }

    public void Fire(bool input)
    {
        if (!input) return;
        if (lockOnEnemies.Count <= 0) return;

        RemoveDestroyedEnemyInLockOn(); //�j�����ꂽ�G���ߑ����X�g�ɂ����ꍇ�A���̃��\�b�h�Ń��X�g����폜
        if (status.GunParam.Bullet <= 0) return; //�c�e���Ȃ��ꍇ�A�U���ł��Ȃ�
        status.GunParam.HittingEnemy.Invoke(status.transform, lockOnEnemies, status.GunParam, status.SmashParam); //�U�������s
        status.SetBullet(-1); //�e�������
        OnTrigger("OnFiring", status.GunParam.Bullet.ToString()); //UI���X�V
        status.GunEffect.Play(); //�G�t�F�N�g���Đ�
    }

    public void Reload(bool input)
    {
        if (!input) return;
        if (reloading) return;

        Task _ = ReloadTime(); //�����[�h���J�n
    }

    private async Task ReloadTime()
    {
        reloading = true;
        await Task.Delay(TimeSpan.FromSeconds(status.PlayerParam.ReloadSpeed)); //�����[�h�����܂ł̑ҋ@���Ԃ̓v���C���[�̃p�����[�^�[�Ɉˑ�
        status.SetBullet(status.GunParam.BulletMax - status.GunParam.Bullet); //����Ȃ����������U�����
        OnTrigger("OnReloading", status.GunParam.Bullet.ToString()); //UI���X�V
        reloading = false;
    }

    //�G�I�u�W�F�N�g�̕ߑ�
    public void EnemyInCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyAct>() == null) return;
            lockOnEnemies.Add(other.GetComponent<EnemyAct>()); //�G�̔�_���[�W�𐧌䂷��N���X���擾
        }
    }

    //�G�I�u�W�F�N�g�̕ߑ�����
    public void EnemyOutCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Remove(other.GetComponent<EnemyAct>()); //TargetingCollider�͈̔͂���O�ꂽ�ꍇ�A���X�g���珜�O
        }
    }

    //�j�����ꂽ�G�����X�g���珜�O
    private void RemoveDestroyedEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
