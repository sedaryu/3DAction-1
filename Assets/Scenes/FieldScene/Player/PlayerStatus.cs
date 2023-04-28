using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MobStatus
{
    //�p�����[�^�[
    public PlayerParam PlayerParam
    {
        get => _playerParam;
    }
    private PlayerParam _playerParam;

    public GunParam GunParam
    {
        get => _gunParam;
    }
    private GunParam _gunParam;

    //�����ݒ�p�����[�^�[
    [SerializeField] private PlayerParam initialPlayerParam;
    [SerializeField] private GunParam initialGunParam;

    protected override void Awake()
    {
        base.Awake();
        _playerParam = new PlayerParam(initialPlayerParam);
        _gunParam = new GunParam(initialGunParam);
    }

    //��_���[�W�̍ۂ�HitPoint�̌��������s���郁�\�b�h
    public override void Damage(float damage)
    {
        _playerParam.HitPoint -= damage;
        if (PlayerParam.HitPoint <= 0) GoToDieStateIfPossible(); //0�ȉ��Ȃ�Ώ�Ԃ�Die�Ɉڍs
    }

    //�U���E�����[�h�̍ۂ�Bullet�̑��������s���郁�\�b�h
    public void SetBullet(int bullet)
    { 
        _gunParam.Bullet += bullet;
    }
}
