using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//飛び道具固有のパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "ProjectileParam", menuName = "Custom/ProjectileParam")]
public class ProjectileParam : ScriptableObject
{
    public float Speed //移動速度
    {
        get => _speed;
    }
    [SerializeField] private float _speed;

    public float Reach //射程
    {
        get => _reach;
    }
    [SerializeField] private float _reach;

    public float Attack //攻撃力
    {
        get => _attack;
    }
    [SerializeField] private float _attack;

    public float Knockback //ノックバック距離
    {
        get => _knockback;
    }
    [SerializeField] private float _knockback;
}
