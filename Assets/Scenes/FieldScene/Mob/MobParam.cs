using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�S�ẴL�����I�u�W�F�N�g�ɋ��ʂ���p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float Speed //�ړ����x
    {
        get => _speed;
    }
    [SerializeField] private float _speed;
}
