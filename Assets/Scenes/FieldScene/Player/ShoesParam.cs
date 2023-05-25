using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全てのキャラオブジェクトに共通するパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "ShoesParam", menuName = "Custom/ShoesParam")]
public class ShoesParam : ScriptableObject
{
    public float Life
    {
        get => _life;
    }
    [SerializeField] private float _life;

    public float MoveSpeed
    {
        get => _moveSpeed;
    }
    [SerializeField] private float _moveSpeed;

    public float AdrenalineSpeed
    {
        get => _adrenalineSpeed;
    }
    [SerializeField] private float _adrenalineSpeed;
}
