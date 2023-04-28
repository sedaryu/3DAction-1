using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵キャラ固有のパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "EnemyParam", menuName = "Custom/EnemyParam")]
public class EnemyParam : MobParam
{
    public float Attack //接触した際受けるダメージ量
    {
        get => _attack;
    }
    [SerializeField] private float _attack = 1;

    public float Weight //重さ
    {
        get => _weight;
    }
    [SerializeField] private float _weight = 1;

    public float Recover //一秒間に回復するHitPointの量
    {
        get => _recover;
    }
    [SerializeField] private float _recover;

    public EnemyParam(EnemyParam initialParam)
    {
        _hitPointMax = initialParam.HitPointMax;
        _hitPoint = initialParam.HitPoint;
        _speedMax = initialParam.SpeedMax;
        _speedMin = initialParam.SpeedMin;
        _attack = initialParam.Attack;
        _weight = initialParam.Weight;
        _recover = initialParam.Recover;
    }
}
