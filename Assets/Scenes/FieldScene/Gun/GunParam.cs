using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public int BulletMax //弾数
    {
        get => _bulletMax;
    }
    [SerializeField] private int _bulletMax;

    public int Bullet //弾数
    {
        get => _bullet;
        set
        {
            if (value <= 0) _bullet = 0;
            else if (_bulletMax <= value) _bullet = _bulletMax;
            else _bullet = value;
        }
    }
    [SerializeField] private int _bullet;

    public GameObject GunPrefab //銃のオブジェクト
    {
        get => _gunPrefab;
    }
    [SerializeField] private GameObject _gunPrefab;

    public GunParam(GunParam initialParam)
    {
        _type = initialParam._type;
        _reach = initialParam._reach;
        _range = initialParam._range;
        _attack = initialParam._attack;
        _knockback = initialParam._knockback;
        _bulletMax = initialParam._bulletMax;
        _bullet = initialParam._bullet;
        _gunPrefab = initialParam._gunPrefab;
    }
}
