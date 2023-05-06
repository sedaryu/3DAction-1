using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// PlayerParam・GunParam・SmashParamを取得し格納する
/// Paramの値を増減させる目的のクラス
/// </Summary>
public class PlayerStatus : MobStatus
{
    //プレイヤーパラメーター
    public PlayerParam PlayerParam
    {
        get => _playerParam;
    }
    private PlayerParam _playerParam;

    //銃パラメーター
    public GunParam GunParam
    {
        get => _gunParam;
    }
    private GunParam _gunParam;

    //スマッシュ技パラメーター
    public SmashParam SmashParam
    {
        get => _smashParam;
    }
    [SerializeField] private SmashParam _smashParam;

    //初期設定パラメーター
    [SerializeField] private PlayerParam initialPlayerParam;
    [SerializeField] private GunParam initialGunParam;

    protected override void Awake()
    {
        base.Awake();
        _playerParam = new PlayerParam(initialPlayerParam);
        _gunParam = new GunParam(initialGunParam);

        SettingGunPrefab(); //銃のオブジェクトを生成し、位置を調整する
    }

    /// <summary>
    /// 銃のオブジェクトを生成し、位置を調整する目的のメソッド
    /// </summary>
    private void SettingGunPrefab()
    {
        GameObject gunPrefab = Instantiate(GunParam.GunPrefab);
        string path = "Armature | Humanoid/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand";
        gunPrefab.transform.parent = GameObject.Find("Player").transform.Find(path);
        gunPrefab.transform.localPosition = new Vector3(0, 0.25f, 0);
        gunPrefab.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        gunPrefab.transform.localScale = new Vector3(3, 3, 3);
    }

    /// <summary>
    /// 受けたダメージぶんHitPointを減少させる目的のメソッド
    /// </summary>
    /// <param name="damage">受けたダメージ量</param>
    /// <returns>ダメージの結果HitPointが0以下になった(死亡したら)true、そうでなければfalseを返す</returns>
    public override bool Damage(float damage)
    {
        _playerParam.HitPoint -= damage;
        if (PlayerParam.HitPoint <= 0) return true;
        return false;
    }

    /// <summary>
    /// 攻撃・リロードの際のBulletの増減を管理する目的のメソッド
    /// </summary>
    /// <param name="bullet">消費また装填した弾薬の量</param>
    public void SetBullet(int bullet)
    { 
        _gunParam.Bullet += bullet;
    }
}
