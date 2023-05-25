using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�S�ẴL�����I�u�W�F�N�g�ɋ��ʂ���p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "ShoesParam", menuName = "Custom/ShoesParam")]
public class ShoesParam : ScriptableObject
{
    public float Life
    {
        get => _life;
    }
    [SerializeField] private float _life;

    public float MoveSpeed
    {
        get => _moveSpeed;
    }
    [SerializeField] private float _moveSpeed;

    public float AdrenalineSpeed
    {
        get => _adrenalineSpeed;
    }
    [SerializeField] private float _adrenalineSpeed;
}
