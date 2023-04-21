using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float HitPoint //0になるととどめを刺される
    {
        get => _hitPoint;
    }
    [SerializeField] private float _hitPoint;

    public float Speed //移動速度
    {
        get => _speed;
    }
    [SerializeField] private float _speed;
}
