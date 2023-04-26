using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G�L�����ŗL�̃p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "EnemyParam", menuName = "Custom/EnemyParam")]
public class EnemyParam : MobParam
{
    public float HitPointMax //HitPoint�̍ő�l
    {
        get => _hitPointMax;
    }
    [SerializeField] private float _hitPointMax;

    public float HitPoint //0�ɂȂ�ƂƂǂ߂��h�����
    {
        get => _hitPoint;
        set
        {
            if (value <= 0) _hitPoint = 0;
            else if (_hitPointMax <= value) _hitPoint = _hitPointMax;
            else _hitPoint = value;
        }
    }
    [SerializeField] private float _hitPoint;

    public float Weight //�d��
    {
        get => _weight;
    }
    [SerializeField] private float _weight = 1;

    public float Recover //��b�Ԃɉ񕜂���HitPoint�̗�
    {
        get => _recover;
    }
    [SerializeField] private float _recover;

    public EnemyParam(EnemyParam initialParam)
    { 
        _speedMax = initialParam.SpeedMax;
        _speedMin = initialParam.SpeedMin;
        _hitPointMax = initialParam.HitPointMax;
        _hitPoint = initialParam.HitPoint;
        _weight = initialParam.Weight;
        _recover = initialParam.Recover;
    }
}
