using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//飛び道具固有のパラメーター(能力値)をこのクラスで定義・管理する
[CreateAssetMenu(fileName = "ProjectileParam", menuName = "Custom/ProjectileParam")]
public class GunParam : ScriptableObject
{
    public enum GunType
    { 
        Pistol,
        SubMachineGun,
        ShotGun,
        Rifle
    }

    public GunType Type //銃の種類
    {
        get => _type;
    }
    [SerializeField] private GunType _type;

    public float Reach //Meshの縦幅
    {
        get => _reach;
    }
    [SerializeField] private float _reach;

    public float Range //Meshの横幅
    {
        get => _range;
    }
    [SerializeField] private float _range;

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

    public HittingEvent HittingEnemy
    {
        get => _hittingEnemy;
    }
    [SerializeField] private HittingEvent _hittingEnemy = new HittingEvent();

    [Serializable]
    public class HittingEvent : UnityEvent<Transform, List<EnemyController>, GunParam> {}
}
