using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MobStatus
{
    //�p�����[�^�[
    public PlayerParam Param
    {
        get => _param;
    }
    private PlayerParam _param;

    //�����ݒ�p�����[�^�[
    [SerializeField] private PlayerParam initialParam;

    protected override void Awake()
    {
        base.Awake();
        _param = new PlayerParam(initialParam);
    }

    //��_���[�W�̍ۂ�HitPoint�̌��������s���郁�\�b�h
    public override void Damage(float damage)
    {
        _param.HitPoint -= damage;
        if (Param.HitPoint <= 0) GoToDieStateIfPossible(); //0�ȉ��Ȃ�Ώ�Ԃ�Die�Ɉڍs
        Debug.Log(Param.HitPoint);
    }
}
