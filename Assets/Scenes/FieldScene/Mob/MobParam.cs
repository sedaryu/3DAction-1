using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�S�ẴL�����I�u�W�F�N�g�ɋ��ʂ���p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float HitPointMax //HitPoint�̍ő�l
    {
        get => _hitPointMax;
    }
    [SerializeField] protected float _hitPointMax;

    public float HitPoint //0�ɂȂ�ƂƂǂ߂��h�����
    {
        get => _hitPoint;
        set
        {
            //0�ȏ�HitPointMax�ȉ��͈̔͂Ɏ��܂�悤����
            if (value <= 0) _hitPoint = 0;
            else if (_hitPointMax <= value) _hitPoint = _hitPointMax;
            else _hitPoint = value;
        }
    }
    [SerializeField] protected float _hitPoint;

    public float SpeedMax //�ō����x
    {
        get => _speedMax;
    }
    [SerializeField] protected float _speedMax;

    public float SpeedMin //�Œᑬ�x
    {
        get => _speedMin;
    }
    [SerializeField] protected float _speedMin;
}
