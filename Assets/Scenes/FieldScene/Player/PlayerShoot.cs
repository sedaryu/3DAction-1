using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //�X�e�[�^�X
    private PlayerStatus status;
    //�R���g���[���[
    private PlayerController controller;

    //TargetingCollider�ŕߑ������G�I�u�W�F�N�g���i�[
    List<EnemyDamage> lockOnEnemies = new List<EnemyDamage>();

    //�����[�h�����ǂ���
    private bool reloading;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //�{�^�����͂����m���A�U�������s
        Fire(controller.InputFiring());
        //�{�^�����͂����m���A�����[�h�����s
        Reload(controller.InputReloading());
    }

    private void Fire(bool input)
    {
        if (!input) return;
        if (lockOnEnemies.Count <= 0) return;

        RemoveDestroyedEnemyInLockOn(); //�j�����ꂽ�G���ߑ����X�g�ɂ����ꍇ�A���̃��\�b�h�Ń��X�g����폜
        if (status.GunParam.Bullet <= 0) { Debug.Log("Empty"); return; } //�c�e���Ȃ��ꍇ�A�U���ł��Ȃ�
        status.GunParam.HittingEnemy.Invoke(this.transform, lockOnEnemies, status.GunParam); //�U�������s
        status.SetBullet(-1); //�e�������
    }

    private void Reload(bool input)
    {
        if (!input) return;
        if (reloading) return;

        reloading = true;
        StartCoroutine(ReloadTime()); //�����[�h�����ҋ@���Ԃ��J�n
    }

    private IEnumerator ReloadTime()
    { 
        yield return new WaitForSeconds(status.PlayerParam.ReloadSpeed); //�����[�h�����܂ł̑ҋ@���Ԃ̓v���C���[�̃p�����[�^�[�Ɉˑ�
        status.SetBullet(status.GunParam.BulletMax - status.GunParam.Bullet); //����Ȃ����������U�����
        reloading = false;
    }

    //�G�I�u�W�F�N�g�̕ߑ�
    public void EnemyInCollider(Collider other)
    {
        Debug.Log("InCollider");
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyDamage>() == null) return;
            lockOnEnemies.Add(other.GetComponent<EnemyDamage>()); //�G�̔�_���[�W�𐧌䂷��N���X���擾
        }
    }

    //�G�I�u�W�F�N�g�̕ߑ�����
    public void EnemyOutCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Remove(other.GetComponent<EnemyDamage>()); //TargetingCollider�͈̔͂���O�ꂽ�ꍇ�A���X�g���珜�O
        }
    }

    //�j�����ꂽ�G�����X�g���珜�O
    private void RemoveDestroyedEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
