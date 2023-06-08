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
public class PlayerParameter : MonoBehaviour, IParametable
{
    private PlayerParam param;

    //スマッシュ
    public float SmashTime { get => param.Smash.Param.SmashTime; }
    public Smash Smash { get => param.Smash; }

    //シューズ
    public Shoes Shoes { get => param.Shoes; }

    //ガン
    public float Range { get => param.Gun.Param.Range; }
    public float Reach { get => param.Gun.Param.Reach; }
    public Gun Gun { get => param.Gun; }
    //ガンエフェクト
    public ParticleSystem GunEffect { get; private set; }

    public float Parameter(string key)
    {
        if (!parameter.ContainsKey(key)) throw new NullReferenceException();
        return parameter[key];
    }
    public bool IsAdrenalinable => parameter["Adrenaline"] > 0;
    private Dictionary<string, float> parameter;

    private void Awake()
    {

    }

    private void Start()
    {
        param = GameObject.Find("ParamReceiver").GetComponent<ParamReceiver>().playerParam;
        SettingParameter();

        SettingGunPrefab(); //銃のオブジェクトを生成し、位置を調整する
        SetEventOnGameClear();
    }

    private void SettingParameter()
    {
        parameter = new Dictionary<string, float>()
        { 
          {"Life", param.Life}, {"LifeMax", Shoes.Param.Life}, 
          {"MoveSpeed", Shoes.Param.MoveSpeed}, {"MoveSpeedMax", Shoes.Param.MoveSpeed},
          {"Adrenaline", 0}, {"AdrenalineMax", 1},
          {"AdrenalineTank", 0}, {"AdrenalineTankMax", 3},
          {"AdrenalineSpeed", Shoes.Param.AdrenalineSpeed}, {"AdrenalineSpeedMax", Shoes.Param.AdrenalineSpeed},
          {"Attack", Gun.Param.Attack}, {"AttackMax", Gun.Param.Attack},
          {"Knockback", Gun.Param.Knockback}, {"KnockbackMax", Gun.Param.Knockback},
          {"Critical", Gun.Param.CriticalMin}, {"CriticalMin", Gun.Param.CriticalMin}, {"CriticalAdd", Gun.Param.CriticalAdd},
          {"Bullet", Gun.Param.Bullet}, {"BulletMax", Gun.Param.Bullet},
          {"ReloadSpeed", Gun.Param.ReloadSpeed}, {"ReloadSpeedMax", Gun.Param.ReloadSpeed}
        };
    }

    /// <summary>
    /// 銃のオブジェクトを生成し、位置を調整する目的のメソッド
    /// </summary>
    private void SettingGunPrefab()
    {
        Gun gunObject = Instantiate(Gun);
        string path = "Armature | Humanoid/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand";
        gunObject.transform.parent = GameObject.Find("Player").transform.Find(path);
        gunObject.transform.localPosition = new Vector3(0, 0.25f, 0);
        gunObject.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        gunObject.transform.localScale = new Vector3(3, 3, 3);
        GunEffect = gunObject.transform.Find("ShotEffect").GetComponent<ParticleSystem>();
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
                if (parameter["Life"] == 0)
                {
                    GameObject.Find("GameController").GetComponent<GameController>().GameOver();
                    Destroy(gameObject);
                }
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

    public void RevertParameter(string key)
    {
        parameter[key] = parameter[key + "Max"];
    }

    public void SetEventOnGameClear()
    {
        GameObject.Find("GameController").GetComponent<GameController>().onGameClear += SetParamToNextScene;
    }

    public void SetParamToNextScene()
    {
        GameObject.Find("GameController").GetComponent<GameController>().life = parameter["Life"];
    }
}
