using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <Summary>
/// PlayerParam・GunParam・SmashParamを取得し格納する
/// Paramの値を増減させる目的のクラス
/// </Summary>
public class PlayerStatus : MonoBehaviour
{
    //状態
    //public Dictionary<string, bool> Staus { get; private set; } = new Dictionary<string, bool>() 
    //{ { "IsMovable", true }, { "IsShootable", true }, { "IsDamageable", true } };
    public bool IsMobable { get; private set; } = true;
    public bool IsShootable { get; private set; } = true;
    public bool IsDamageable { get; private set; } = true;

    public IEnumerator WaitForStatusTransition(bool state, float time)
    {
        state = false;
        yield return new WaitForSeconds(time);
        state = true;
    }

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

    //銃のエフェクト
    public ParticleSystem GunEffect
    {
        get => _gunEffect;
    }
    private ParticleSystem _gunEffect;

    protected void Awake()
    {
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
        _gunEffect = gunPrefab.transform.Find("ShotEffect").GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// 受けたダメージぶんHitPointを減少させる目的のメソッド
    /// </summary>
    /// <param name="damage">受けたダメージ量</param>
    /// <returns>ダメージの結果HitPointが0以下になった(死亡したら)true、そうでなければfalseを返す</returns>
    public void Damage(float damage)
    {
        _playerParam.HitPoint -= damage;
        if (PlayerParam.HitPoint <= 0) Destroy(gameObject);
    }

    /// <summary>
    /// 攻撃・リロードの際のBulletの増減を管理する目的のメソッド
    /// </summary>
    /// <param name="bullet">消費また装填した弾薬の量</param>
    public void SetBullet(float bullet)
    { 
        _gunParam.Bullet += (int)bullet;
    }
}
