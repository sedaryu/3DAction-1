using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全てのキャラオブジェクトに共通するパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float SpeedMax //最高速度
    {
        get => _speedMax;
    }
    [SerializeField] protected float _speedMax;

    public float SpeedMin //最低速度
    {
        get => _speedMin;
    }
    [SerializeField] protected float _speedMin;
}
