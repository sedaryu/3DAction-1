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

    //TargetingCollider�ŕߑ������G�I�u�W�F�N�g���i�[
    List<EnemyDamage> lockOnEnemies = new List<EnemyDamage>();

    //�����[�h�����ǂ���
    private bool reloading;

    void Awake()
    {
        _param = new GunParam(initialParam);
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //�{�^�����͂����m���A�U�������s
        if (Input.GetButtonDown("Fire1") && lockOnEnemies.Count > 0) //TargetingCollider�ŕߑ������G�����Ȃ��ꍇ�͍U�����s��Ȃ�
        {
            RemoveDeadEnemyInLockOn(); //���S�����G���ߑ����X�g�ɂ����ꍇ�A���̃��\�b�h�Ń��X�g����폜
            if (Param.Bullet <= 0) { Debug.Log("Empty"); return; } //�c�e���Ȃ��ꍇ�A�U���ł��Ȃ�
            Param.HittingEnemy.Invoke(this.transform, lockOnEnemies, Param); //�U�������s
            _param.Bullet--; //�e�������
        }

        //�{�^�����͂����m���A�����[�h�����s
        if (Input.GetButtonDown("Reload") && !reloading) //�����[�h���͍s���Ȃ�
        {
            reloading = true;
            StartCoroutine(Reloading()); //�����[�h�����ҋ@���Ԃ��J�n
        }
    }

    private IEnumerator Reloading()
    { 
        yield return new WaitForSeconds(status.Param.ReloadSpeed); //�����[�h�����܂ł̑ҋ@���Ԃ̓v���C���[�̃p�����[�^�[�Ɉˑ�
        Param.Bullet += Param.BulletMax - Param.Bullet; //����Ȃ����������U�����
        reloading = false;
        Debug.Log("Reloaded");
    }

    //�G�I�u�W�F�N�g�̕ߑ�
    public void EnemyInCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
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

    //���S�����G�����X�g���珜�O
    private void RemoveDeadEnemyInLockOn()
    {
        lockOnEnemies.RemoveAll(x => x == null);
    }
}
