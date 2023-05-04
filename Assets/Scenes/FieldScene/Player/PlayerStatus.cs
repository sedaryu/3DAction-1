using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MobStatus
{
    //パラメーター
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

    //初期設定パラメーター
    [SerializeField] private PlayerParam initialPlayerParam;
    [SerializeField] private GunParam initialGunParam;

    public bool IsDamagable => (state == StateEnum.Normal); //状態がNormalであればtrueを返す
    public bool IsCombating => (state == StateEnum.NoMoveInvincible); //状態がNoMoveInvincibleであればtrueを返す

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

    public void GoToNoMoveInvincibleStateIfPossible() //状態がNoMoveInvincibleに遷移する
    {
        if (state == StateEnum.Die || state == StateEnum.NoMoveInvincible) return;
        state = StateEnum.NoMoveInvincible;
    }

    //被ダメージの際のHitPointの減少を実行するメソッド
    public override void Damage(float damage)
    {
        _playerParam.HitPoint -= damage;
        if (PlayerParam.HitPoint <= 0) GoToDieStateIfPossible(); //0以下ならば状態がDieに移行
    }

    //攻撃・リロードの際のBulletの増減を実行するメソッド
    public void SetBullet(int bullet)
    { 
        _gunParam.Bullet += bullet;
    }
}
