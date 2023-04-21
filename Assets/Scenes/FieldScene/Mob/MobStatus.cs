using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatus
{
    protected enum StateEnum //�L�����N�^�[�̏��
    {
        Normal, //�ʏ펞(Attack��Die�Ɉڍs�\)
        Damage, //�_���[�W��
        Groggy, //HitPoint��0�̎�(�Ƃǂ߂��h�����)
        Die //���S��(�ǂ̏�Ԃɂ��ڍs���Ȃ�)
    }

    protected StateEnum state = StateEnum.Normal; //�����l��Normal

    public bool IsMovable => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�

    public bool IsDamageble => (state == StateEnum.Damage);

    public void GoToDamageStateIfPossible() //��Ԃ�Damage�ɑJ�ڂ���
    {
        state = StateEnum.Damage;
    }

    public void GoToNormalStateIfPossible() //��Ԃ�Normal�ɑJ�ڂ���
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Normal;
    }
}
