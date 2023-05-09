using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effecter;

public class EnemyGroggy
{
    //�X�e�[�^�X
    private EnemyStatus status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;

    public EnemyGroggy(EnemyStatus _status, MobEffecter _effecter)
    { 
        status = _status;
        effecter = _effecter;
    }

    public void Groggy(SmashParam smash)
    {
        status.GoToDieStateIfPossible(); //0�ȉ��Ȃ�Ώ�Ԃ�Die�Ɉڍs
        GameObject smashCollider = effecter.InstanceEffect(smash.SmashCollider); //SmashCollider�𐶐�
        smashCollider.transform.parent = status.transform; //�q�I�u�W�F�N�g��
        smashCollider.GetComponent<Smash>().StartTimer(smash.DestroyTime);
    }
}
