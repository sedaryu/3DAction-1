using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

/// <Summary>
/// PlayerParam�EGunParam�ESmashParam���擾���i�[����
/// Param�̒l�𑝌�������ړI�̃N���X
/// </Summary>
public class PlayerParameter : MonoBehaviour
{
    //�X�}�b�V���Z�p�����[�^�[
    public SmashParam SmashParam
    {
        get => _smashParam;
    }
    [SerializeField] private SmashParam _smashParam;

    //�����ݒ�p�����[�^�[
    [SerializeField] private PlayerParam initialPlayerParam;

    public float Range
    { 
        get => initialGunParam.Range;
    }
    public float Reach
    {
        get => initialGunParam.Reach;
    }
    [SerializeField] private GunParam initialGunParam;


    public float Parameter(string key)
    {
        if (!parameter.ContainsKey(key)) throw new NullReferenceException();
        return parameter[key];
    }
    public bool IsAdrenalinable => parameter["Adrenaline"] > 0;
    private Dictionary<string, float> parameter;

    //�e�̃G�t�F�N�g
    public ParticleSystem GunEffect
    {
        get => _gunEffect;
    }
    private ParticleSystem _gunEffect;

    protected void Awake()
    {
        SettingParameter();

        SettingGunPrefab(); //�e�̃I�u�W�F�N�g�𐶐����A�ʒu�𒲐�����
    }

    private void SettingParameter()
    {
        parameter = new Dictionary<string, float>()
        { 
          {"Life", initialPlayerParam.Life}, {"LifeMax", initialPlayerParam.Life}, 
          {"MoveSpeed", initialPlayerParam.MoveSpeed}, {"MoveSpeedMax", initialPlayerParam.MoveSpeed},
          {"Adrenaline", 0}, {"AdrenalineMax", 1},
          {"AdrenalineTank", 0}, {"AdrenalineTankMax", 3},
          {"AdrenalineSpeed", initialPlayerParam.AdrenalineSpeed}, {"AdrenalineSpeedMax", initialPlayerParam.AdrenalineSpeed},
          {"Attack", initialGunParam.Attack}, {"AttackMax", initialGunParam.Attack},
          {"Knockback", initialGunParam.Knockback}, {"KnockbackMax", initialGunParam.Knockback},
          {"Critical", initialGunParam.CriticalMin}, {"CriticalMin", initialGunParam.CriticalMin}, {"CriticalAdd", initialGunParam.CriticalAdd},
          {"Bullet", initialGunParam.Bullet}, {"BulletMax", initialGunParam.Bullet},
          {"ReloadSpeed", initialGunParam.ReloadSpeed}, {"ReloadSpeedMax", initialGunParam.ReloadSpeed}
        };
    }

    /// <summary>
    /// �e�̃I�u�W�F�N�g�𐶐����A�ʒu�𒲐�����ړI�̃��\�b�h
    /// </summary>
    private void SettingGunPrefab()
    {
        GameObject gunPrefab = Instantiate(initialGunParam.GunPrefab);
        string path = "Armature | Humanoid/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand";
        gunPrefab.transform.parent = GameObject.Find("Player").transform.Find(path);
        gunPrefab.transform.localPosition = new Vector3(0, 0.25f, 0);
        gunPrefab.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        gunPrefab.transform.localScale = new Vector3(3, 3, 3);
        _gunEffect = gunPrefab.transform.Find("ShotEffect").GetComponent<ParticleSystem>();
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
