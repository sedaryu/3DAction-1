using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G�L�����ŗL�̃p�����[�^�[(�\�͒l)�����̃N���X�Œ�`�E�Ǘ�����
[CreateAssetMenu(fileName = "EnemyParam", menuName = "Custom/EnemyParam")]
public class EnemyParam : MobParam
{
    public float HitPoint //0�ɂȂ�ƂƂǂ߂��h�����
    {
        get => _hitPoint;
    }
    [SerializeField] private float _hitPoint;
}
