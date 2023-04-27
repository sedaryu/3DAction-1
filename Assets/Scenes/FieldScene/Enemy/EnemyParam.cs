using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G�L�����ŗL�̃p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "EnemyParam", menuName = "Custom/EnemyParam")]
public class EnemyParam : MobParam
{
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
        _hitPointMax = initialParam.HitPointMax;
        _hitPoint = initialParam.HitPoint;
        _speedMax = initialParam.SpeedMax;
        _speedMin = initialParam.SpeedMin;
        _weight = initialParam.Weight;
        _recover = initialParam.Recover;
    }
}
