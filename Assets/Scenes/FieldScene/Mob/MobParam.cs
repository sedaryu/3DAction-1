using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全てのキャラオブジェクトに共通するパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float HitPointMax //HitPointの最大値
    {
        get => _hitPointMax;
    }
    [SerializeField] protected float _hitPointMax;

    public float HitPoint //0になるととどめを刺される
    {
        get => _hitPoint;
        set
        {
            //0以上HitPointMax以下の範囲に収まるよう制御
            if (value <= 0) _hitPoint = 0;
            else if (_hitPointMax <= value) _hitPoint = _hitPointMax;
            else _hitPoint = value;
        }
    }
    [SerializeField] protected float _hitPoint;

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
