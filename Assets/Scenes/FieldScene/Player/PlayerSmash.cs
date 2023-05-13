using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <Summary>
/// �v���C���[�̃X�}�b�V���U������������N���X
/// </Summary>
public class PlayerSmash
{
    //�X�e�[�^�X
    private PlayerParameter status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;

    //�擾�����R���_�[�I�u�W�F�N�g
    private List<GameObject> colliders = new List<GameObject>();

    public PlayerSmash(PlayerParameter _status, MobEffecter _effecter)
    {
        status = _status;
        effecter = _effecter;
    }

    /// <summary>
    /// �X�}�b�V���R���_�[�ɐڐG�����ہA���̃R���_�[�̃T�C�Y���g�債�A
    /// ���̃R���_�[��colliders�ɒǉ�����
    /// </summary>
    /// <param name="other">�ڐG�����R���_�[</param>
    public void PlayerInSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = Vector3.one * status.SmashParam.RangeMax; //�R���_�[���g��
            colliders.Add(other.transform.parent.gameObject); //�ǉ�
        }
    }

    /// <summary>
    /// �X�}�b�V���R���_�[���痣�ꂽ�ہA���̃R���_�[�̃T�C�Y�����ɖ߂��A
    /// ���̃R���_�[��colliders���珜�O����
    /// </summary>
    /// <param name="other">���ꂽ�R���_�[</param>
    public void PlayerOutSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = Vector3.one * status.SmashParam.RangeMin; //�R���_�[�̃T�C�Y�����ɖ߂�
            colliders.Remove(other.transform.parent.gameObject); //���O
        }
    }

    /// <summary>
    /// �X�}�b�V���U�������s���邽�߂̃��\�b�h
    /// </summary>
    /// <param name="input">���͂��ꂽ�ꍇ��true</param>
    public void Smash(bool input)
    {
        if (!input) return;
        if (status.IsNoMoveInvincible) return; //�ړ��s���G�̍ۂ͎��s�ł��Ȃ�
        if (colliders.Count <= 0) return; //colliders�ɉ����܂܂�Ă��Ȃ��ꍇ�͎��s�ł��Ȃ�
        if (RemoveDestroyedCollider()) return; //�j�����ꂽ�R���_�[�����O
        foreach (GameObject x in colliders) 
        {
            Task _0 = x.GetComponentInChildren<SmashAct>().PlayerSmashEnemies(status.SmashParam); //�R���_�[���̓G�S�ĂɃX�}�b�V���U��
            Task _1 = SmashTime(x.transform.position, x.transform.rotation); //
        }
    }

    /// <summary>
    /// �X�}�b�V���U���̍ۂ̃v���C���[��state�J�ځA
    /// �A�j���[�V�������s�A�G�t�F�N�g�������s�����\�b�h
    /// </summary>
    /// <param name="position">�G�t�F�N�g�𐶐�����ꏊ</param>
    /// <param name="rotation">�G�t�F�N�g�̊p�x</param>
    private async Task SmashTime(Vector3 position, Quaternion rotation)
    {
        status.Animator.SetTrigger("StartSmash");
        status.GoToNoMoveInvincibleStateIfPossible();
        await Task.Delay(TimeSpan.FromSeconds(status.SmashParam.SmashTime * 0.7f));
        //await Task.Delay((int)(status.SmashParam.SmashTime * 700));
        status.Animator.SetTrigger("FinishSmash");
        await Task.Delay(TimeSpan.FromSeconds(status.SmashParam.SmashTime * 0.3f));
        //await Task.Delay((int)(status.SmashParam.SmashTime * 300));
        effecter.InstanceEffect(status.SmashParam.SmashEffect, position, rotation);
        status.GoToNormalStateIfPossible();
    }

    //�j�����ꂽ�R���_�[�����X�g���珜�O
    private bool RemoveDestroyedCollider()
    {
        colliders.RemoveAll(x => x == null);
        if (colliders.Count == 0) return true;
        return false;
    }
}
