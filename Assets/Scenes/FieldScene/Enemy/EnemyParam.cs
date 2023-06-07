using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵キャラ固有のパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "EnemyParam", menuName = "Custom/EnemyParam")]
public class EnemyParam : ScriptableObject
{
    public string Name
    {
        get => _name;
    }
    [SerializeField] private string _name;

    public float HitPoint
    {
        get => _hitPoint;
    }
    [SerializeField] private float _hitPoint = 10;

    public float MoveSpeedMax
    {
        get => _moveSpeedMax;
    }
    [SerializeField] private float _moveSpeedMax = 1;

    public float MoveSpeedMin
    {
        get => _moveSpeedMin;
    }
    [SerializeField] private float _moveSpeedMin = 0.5f;

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

    public float HealSpeed //一秒間に回復するHitPointの量
    {
        get => _healSpeed;
    }
    [SerializeField] private float _healSpeed;

    public string AttackKey
    {
        get => _attackKey;
    }
    [SerializeField] private string _attackKey;

    public int Rank
    {
        get => _rank;
    }
    [SerializeField] private int _rank;

    public EnemyType Type
    {
        get => _type;
    }
    [SerializeField] private EnemyType _type;

    public enum EnemyType
    { 
        Normal,
        Mini
    }
}
