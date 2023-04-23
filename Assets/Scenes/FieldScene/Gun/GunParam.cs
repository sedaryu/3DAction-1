using System;
using System.Collections;
using System.Collections.Generic;
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

    public HittingEvent HittingEnemy
    {
        get => _hittingEnemy;
    }
    [SerializeField] private HittingEvent _hittingEnemy = new HittingEvent();

    [Serializable]
    public class HittingEvent : UnityEvent<Transform, List<EnemyController>, GunParam> {}
}
