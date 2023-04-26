using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵キャラ固有のパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "EnemyParam", menuName = "Custom/EnemyParam")]
public class EnemyParam : MobParam
{
    public float HitPointMax
    {
        get => _hitPointMax;
    }
    [SerializeField] private float _hitPointMax;

    public float HitPoint //0になるととどめを刺される
    {
        get => _hitPoint;
        set
        {
            if (value <= 0) _hitPoint = 0;
            else if (_hitPointMax <= value) _hitPoint = _hitPointMax;
            else _hitPoint = value;
        }
    }
    [SerializeField] private float _hitPoint;

    public float Weight //重さ
    {
        get => _weight;
    }
    [SerializeField] private float _weight = 1;

    public EnemyParam(EnemyParam initialParam)
    { 
        _speed = initialParam.Speed;
        _hitPointMax = initialParam.HitPointMax;
        _hitPoint = initialParam.HitPoint;
        _weight = initialParam.Weight;
    }
}
