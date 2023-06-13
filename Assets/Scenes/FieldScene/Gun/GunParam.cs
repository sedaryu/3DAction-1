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

    public string Name
    {
        get => _name;
    }
    [SerializeField] private string _name;

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

    public float CriticalMin //�Œ�N���e�B�J����
    {
        get => _criticalMin;
    }
    [SerializeField] private float _criticalMin;

    public float CriticalAdd //�A�h���i�����ˑ��̃N���e�B�J����
    {
        get => _criticalAdd;
    }
    [SerializeField] private float _criticalAdd;

    public int BulletMax //�e��
    {
        get => _bulletMax;
    }
    [SerializeField] private int _bulletMax;

    public int Bullet //�e��
    {
        get => _bullet;
    }
    [SerializeField] private int _bullet;

    public float ReloadSpeed //�����[�h�̑���
    {
        get => _reloadSpeed;
    }
    [SerializeField] private float _reloadSpeed;

    public int Prise //�l�i
    {
        get => _prise;
    }
    [SerializeField] private int _prise;
}
