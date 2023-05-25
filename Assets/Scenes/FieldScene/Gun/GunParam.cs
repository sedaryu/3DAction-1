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

    public float CriticalMin //最低クリティカル率
    {
        get => _criticalMin;
    }
    [SerializeField] private float _criticalMin;

    public float CriticalAdd //アドレナリン依存のクリティカル率
    {
        get => _criticalAdd;
    }
    [SerializeField] private float _criticalAdd;

    public int BulletMax //弾数
    {
        get => _bulletMax;
    }
    [SerializeField] private int _bulletMax;

    public int Bullet //弾数
    {
        get => _bullet;
    }
    [SerializeField] private int _bullet;

    public float ReloadSpeed //リロードの速さ
    {
        get => _reloadSpeed;
    }
    [SerializeField] private float _reloadSpeed;
}
