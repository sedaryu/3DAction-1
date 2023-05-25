using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

/// <Summary>
/// PlayerParam・GunParam・SmashParamを取得し格納する
/// Paramの値を増減させる目的のクラス
/// </Summary>
public class PlayerParameter : MonoBehaviour
{
    //スマッシュ
    public float SmashTime { get => smash.Param.SmashTime; }
    public Smash Smash { get => smash; }
    [SerializeField] private Smash smash;

    //シューズ
    [SerializeField] private Shoes shoes;

    //ガン
    public float Range { get => gun.Param.Range; }
    public float Reach { get => gun.Param.Reach; }
    //ガンエフェクト
    public ParticleSystem GunEffect { get => _gunEffect; }
    private ParticleSystem _gunEffect;
    [SerializeField] private Gun gun;

    public float Parameter(string key)
    {
        if (!parameter.ContainsKey(key)) throw new NullReferenceException();
        return parameter[key];
    }
    public bool IsAdrenalinable => parameter["Adrenaline"] > 0;
    private Dictionary<string, float> parameter;

    protected void Awake()
    {
        SettingParameter();

        SettingGunPrefab(); //銃のオブジェクトを生成し、位置を調整する
    }

    private void SettingParameter()
    {
        parameter = new Dictionary<string, float>()
        { 
          {"Life", shoes.Param.Life}, {"LifeMax", shoes.Param.Life}, 
          {"MoveSpeed", shoes.Param.MoveSpeed}, {"MoveSpeedMax", shoes.Param.MoveSpeed},
          {"Adrenaline", 0}, {"AdrenalineMax", 1},
          {"AdrenalineTank", 0}, {"AdrenalineTankMax", 3},
          {"AdrenalineSpeed", shoes.Param.AdrenalineSpeed}, {"AdrenalineSpeedMax", shoes.Param.AdrenalineSpeed},
          {"Attack", gun.Param.Attack}, {"AttackMax", gun.Param.Attack},
          {"Knockback", gun.Param.Knockback}, {"KnockbackMax", gun.Param.Knockback},
          {"Critical", gun.Param.CriticalMin}, {"CriticalMin", gun.Param.CriticalMin}, {"CriticalAdd", gun.Param.CriticalAdd},
          {"Bullet", gun.Param.Bullet}, {"BulletMax", gun.Param.Bullet},
          {"ReloadSpeed", gun.Param.ReloadSpeed}, {"ReloadSpeedMax", gun.Param.ReloadSpeed}
        };
    }

    /// <summary>
    /// 銃のオブジェクトを生成し、位置を調整する目的のメソッド
    /// </summary>
    private void SettingGunPrefab()
    {
        Gun gunObject = Instantiate(gun);
        string path = "Armature | Humanoid/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand";
        gunObject.transform.parent = GameObject.Find("Player").transform.Find(path);
        gunObject.transform.localPosition = new Vector3(0, 0.25f, 0);
        gunObject.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        gunObject.transform.localScale = new Vector3(3, 3, 3);
        _gunEffect = gunObject.transform.Find("ShotEffect").GetComponent<ParticleSystem>();
    }

    public void ChangeParameter(string key, float param)
    {
        if (!parameter.ContainsKey(key)) throw new NullReferenceException();
        if (parameter[key] + param < 0) parameter[key] = 0;
        else if (parameter[key + "Max"] < parameter[key] + param) parameter[key] = parameter[key + "Max"];
        else parameter[key] += param;

        EffectOfParameterChange(key);
    }

    public void SetParameter(string key, float param)
    {
        if (!parameter.ContainsKey(key)) throw new NullReferenceException();
        if (param < 0) parameter[key] = 0;
        else if (parameter[key + "Max"] < param) parameter[key] = parameter[key + "Max"];
        else parameter[key] = param;

        EffectOfParameterChange(key);
    }

    public void EffectOfParameterChange(string key)
    {
        switch (key)
        {
            case "Life":
                if (parameter["Life"] == 0) Destroy(gameObject);
                break;

            case "MoveSpeed":
                if (parameter["MoveSpeed"] <= parameter["MoveSpeedMax"] * 0.15f) parameter["MoveSpeed"] = parameter["MoveSpeedMax"] * 0.15f;
                break;

            case "Adrenaline":
                if (parameter["Adrenaline"] == 1)
                { 
                    parameter["Adrenaline"] = 0.5f; 
                    parameter["AdrenalineTank"] = 
                    parameter["AdrenalineTank"] + 1 >= parameter["AdrenalineTankMax"] ? parameter["AdrenalineTankMax"] : parameter["AdrenalineTank"] + 1;
                }
                parameter["Critical"] = parameter["CriticalMin"] + (parameter["CriticalAdd"] * parameter["Adrenaline"]);
                break;

            default:
                break;
        }
    }

    public void RevertParameter(string key, float param)
    {
        parameter[key] = parameter[key + "Max"];
    }
}
