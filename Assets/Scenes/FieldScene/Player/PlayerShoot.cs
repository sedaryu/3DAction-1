using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class PlayerShoot : Act
{
    //�G�ւ̍U������(�e���Ƃɕς��)
    public event Func<Vector3, List<EnemyReferenced>, float, float, Transform> hittingEnemy;
    //�U�����̃v���C���[�̐U�����
    public event Action<Transform> lookAt;

    //�����[�h�����ǂ���
    private bool reloading;

    public void Fire(Vector3 position, List<EnemyReferenced> enemies, int bullet, float knockback, float attack)
    {
        if (enemies.Count <= 0) return;

        //RemoveDestroyedEnemyInLockOn(); //�j�����ꂽ�G���ߑ����X�g�ɂ����ꍇ�A���̃��\�b�h�Ń��X�g����폜
        if (bullet <= 0) return; //�c�e���Ȃ��ꍇ�A�U���ł��Ȃ�
        Transform enemy = hittingEnemy.Invoke(position, enemies, knockback, attack); //�U�������s
        lookAt.Invoke(enemy); //�U�������G�̕�����U�����
        OnTrigger("SetBullet", -1); //�e�������
        OnTrigger("UpdateBulletUI", (bullet - 1).ToString()); //UI���X�V
        OnTrigger("GunEffectPlay"); //�G�t�F�N�g���Đ�
    }

    public void Reload(float reloadSpeed, int bulletMax, int bullet)
    {
        if (reloading) return;

        Task _ = ReloadTime(reloadSpeed, bulletMax, bullet); //�����[�h���J�n
    }

    private async Task ReloadTime(float reloadSpeed, int bulletMax, int bullet)
    {
        reloading = true;
        await Task.Delay(TimeSpan.FromSeconds(reloadSpeed)); //�����[�h�����܂ł̑ҋ@���Ԃ̓v���C���[�̃p�����[�^�[�Ɉˑ�
        OnTrigger("SetBullet", bulletMax - bullet); //����Ȃ����������U�����
        OnTrigger("UpdateBulletUI", bulletMax.ToString()); //UI���X�V
        reloading = false;
    }
}
