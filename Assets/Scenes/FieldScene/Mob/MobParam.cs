using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全てのキャラオブジェクトに共通するパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float Speed //移動速度
    {
        get => _speed;
    }
    [SerializeField] private float _speed;
}
