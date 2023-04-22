using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ѓ���ŗL�̃p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "ProjectileParam", menuName = "Custom/ProjectileParam")]
public class ProjectileParam : ScriptableObject
{
    public float Speed //�ړ����x
    {
        get => _speed;
    }
    [SerializeField] private float _speed;

    public float Reach //�˒�
    {
        get => _reach;
    }
    [SerializeField] private float _reach;

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
}
