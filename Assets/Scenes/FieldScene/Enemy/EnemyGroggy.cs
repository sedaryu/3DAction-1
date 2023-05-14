using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effecter;

public class EnemyGroggy
{
    //�X�e�[�^�X
    private EnemyParameter status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;

    public EnemyGroggy(EnemyParameter _status, MobEffecter _effecter)
    { 
        status = _status;
        effecter = _effecter;
    }

    public void Groggy(SmashParam smash)
    {
        status.GoToDieStateIfPossible(); //0�ȉ��Ȃ�Ώ�Ԃ�Die�Ɉڍs
        GameObject smashCollider = effecter.InstanceEffect(smash.SmashCollider); //SmashCollider�𐶐�
        smashCollider.transform.parent = status.transform; //�q�I�u�W�F�N�g��
        smashCollider.GetComponent<SmashAct>().StartTimer(smash.DestroyTime, smash.RangeMin);
    }
}
