using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//��ѓ���ŗL�̃p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
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

    public GunType Type //�e�̎��
    {
        get => _type;
    }
    [SerializeField] private GunType _type;

    public float Reach //Mesh�̏c��
    {
        get => _reach;
    }
    [SerializeField] private float _reach;

    public float Range //Mesh�̉���
    {
        get => _range;
    }
    [SerializeField] private float _range;

    public float Attack //�U����
    {
        get => _attack;
    }
    [SerializeField] private float _attack;

    public float Knockback //�m�b�N�o�b�N����
    {
        get => _knockback;
    }
    [SerializeField] private float _knockback;

    public int BulletMax //�e��
    {
        get => _bulletMax;
    }
    [SerializeField] private int _bulletMax;

    public int Bullet //�e��
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

    public GameObject GunPrefab //�e�̃I�u�W�F�N�g
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
