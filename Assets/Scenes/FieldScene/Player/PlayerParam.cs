using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全てのキャラオブジェクトに共通するパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "PlayerParam", menuName = "Custom/PlayerParam")]
public class PlayerParam : MobParam
{
    public float ReloadSpeed //リロードの速さ
    {
        get => _reloadSpeed;
    }
    [SerializeField] private float _reloadSpeed;

    public PlayerParam(PlayerParam initialParam)
    {
        _hitPointMax = initialParam.HitPointMax;
        _hitPoint = initialParam.HitPoint;
        _speedMax = initialParam.SpeedMax;
        _speedMin = initialParam.SpeedMin;
        _reloadSpeed = initialParam.ReloadSpeed;
    }
}
