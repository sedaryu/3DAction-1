using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�S�ẴL�����I�u�W�F�N�g�ɋ��ʂ���p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
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
