using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵キャラ固有のパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "EnemyParam", menuName = "Custom/EnemyParam")]
public class EnemyParam : MobParam
{
    public float HitPoint //0になるととどめを刺される
    {
        get => _hitPoint;
    }
    [SerializeField] private float _hitPoint;
}
