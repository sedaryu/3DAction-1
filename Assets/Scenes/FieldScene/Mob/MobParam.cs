using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float HitPoint //0�ɂȂ�ƂƂǂ߂��h�����
    {
        get => _hitPoint;
    }
    [SerializeField] private float _hitPoint;

    public float Speed //�ړ����x
    {
        get => _speed;
    }
    [SerializeField] private float _speed;
}
