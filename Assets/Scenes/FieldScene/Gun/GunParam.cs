using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//ςΡΉοΕLΜp[^[(\Νl)π±ΜNXΕθ`EΗ·ι
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

    public GunType Type //eΜνή
    {
        get => _type;
    }
    [SerializeField] private GunType _type;

    public float Reach //MeshΜc
    {
        get => _reach;
    }
    [SerializeField] private float _reach;

    public float Range //MeshΜ‘
    {
        get => _range;
    }
    [SerializeField] private float _range;

    public float Attack //UΝ
    {
        get => _attack;
    }
    [SerializeField] private float _attack;

    public float Knockback //mbNobN£
    {
        get => _knockback;
    }
    [SerializeField] private float _knockback;

    public int BulletMax //e
    {
        get => _bulletMax;
    }
    [SerializeField] private int _bulletMax;

    public int Bullet //e
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

    public GameObject GunPrefab //eΜIuWFNg
    {
        get => _gunPrefab;
    }
    [SerializeField] private GameObject _gunPrefab;

    public HittingEvent HittingEnemy
    {
        get => _hittingEnemy;
    }
    [SerializeField] private HittingEvent _hittingEnemy = new HittingEvent();

    [Serializable]
    public class HittingEvent : UnityEvent<Transform, List<EnemyDamage>, GunParam, SmashParam> {}

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
        _hittingEnemy = initialParam.HittingEnemy;
    }
}
