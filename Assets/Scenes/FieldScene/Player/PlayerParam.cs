using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�S�ẴL�����I�u�W�F�N�g�ɋ��ʂ���p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "PlayerParam", menuName = "Custom/PlayerParam")]
public class PlayerParam : MobParam
{
    public float ReloadSpeed //�����[�h�̑���
    {
        get => _reloadSpeed;
    }
    [SerializeField] private float _reloadSpeed;

    public PlayerParam(PlayerParam initialParam)
    {
        _hitPointMax = initialParam.HitPointMax;
        _hitPoint = initialParam.HitPoint;
        _speedMax = initialParam.SpeedMax;
        _speedMin = initialParam.SpeedMin;
        _reloadSpeed = initialParam.ReloadSpeed;
    }
}
