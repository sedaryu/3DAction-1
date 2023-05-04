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

    public bool IsDamagable => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�
    public bool IsCombating => (state == StateEnum.NoMoveInvincible); //��Ԃ�NoMoveInvincible�ł����true��Ԃ�

    protected override void Awake()
    {
        base.Awake();
        _playerParam = new PlayerParam(initialPlayerParam);
        _gunParam = new GunParam(initialGunParam);
        SettingGunPrefab();
    }

    private void SettingGunPrefab()
    {
        GameObject gunPrefab = Instantiate(GunParam.GunPrefab);
        string path = "Armature | Humanoid/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand";
        gunPrefab.transform.parent = GameObject.Find("Player").transform.Find(path);
        gunPrefab.transform.localPosition = new Vector3(0, 0.25f, 0);
        gunPrefab.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        gunPrefab.transform.localScale = new Vector3(3, 3, 3);
    }

    public void GoToNoMoveInvincibleStateIfPossible() //��Ԃ�NoMoveInvincible�ɑJ�ڂ���
    {
        if (state == StateEnum.Die || state == StateEnum.NoMoveInvincible) return;
        state = StateEnum.NoMoveInvincible;
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
