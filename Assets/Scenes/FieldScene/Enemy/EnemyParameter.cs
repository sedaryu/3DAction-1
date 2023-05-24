using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : MonoBehaviour
{
    //初期設定パラメーター
    [SerializeField] private EnemyParam initialParam;

    public string AttackKey { get => initialParam.AttackKey; }

    public float Parameter(string key)
    {
        if (!parameter.ContainsKey(key)) return -1;
        return parameter[key];
    }
    public float PercentageParameter(string key)
    {
        if (!parameter.ContainsKey(key)) return -1;
        return parameter[key] / parameter[key + "Max"];
    }
    private Dictionary<string, float> parameter;

    void Awake()
    {
        SettingParameter();
    }

    private void SettingParameter()
    {
        float moveSpeed = Random.Range(initialParam.MoveSpeedMin, initialParam.MoveSpeedMax);

        parameter = new Dictionary<string, float>()
        {
          {"HitPoint", initialParam.HitPoint}, {"HitPointMax", initialParam.HitPoint},
          {"MoveSpeed", moveSpeed}, {"MoveSpeedMax", moveSpeed},
          {"Attack", initialParam.Attack}, {"AttackMax", initialParam.Attack},
          {"Weight", initialParam.Weight}, {"WeightMax", initialParam.Weight},
          {"HealSpeed", initialParam.HealSpeed}, {"HealSpeedMax", initialParam.HealSpeed}
        };
    }

    public void SetParameter(string key, float param)
    {
        if (parameter[key] + param < 0) parameter[key] = 0;
        else if (parameter[key + "Max"] < parameter[key] + param) parameter[key] = parameter[key + "Max"];
        else parameter[key] += param;
    }

    public void RevertParameter(string key, float param)
    {
        parameter[key] = parameter[key + "Max"];
    }
}
