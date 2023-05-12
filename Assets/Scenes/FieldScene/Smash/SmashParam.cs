using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmashParam", menuName = "Custom/SmashParam")]
public class SmashParam : ScriptableObject
{
    public float SmashTime //���o����
    {
        get => _smashTime;
    }
    [SerializeField] private float _smashTime;

    public float DestroyTime //SmashCollider���j�󂳂��܂ł̎���
    {
        get => _destroyTime;
    }
    [SerializeField] private float _destroyTime;

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

    public float RangeMin
    {
        get => _rangeMin;
    }
    [SerializeField] private float _rangeMin;

    public float RangeMax
    {
        get => _rangeMax;
    }
    [SerializeField] private float _rangeMax;

    public GameObject SmashCollider //�R���_�[�I�u�W�F�N�g
    {
        get => _smashCollider;
    }
    [SerializeField] private GameObject _smashCollider;

    public GameObject SmashEffect //�X�}�b�V���U�����̃G�t�F�N�g
    {
        get => _smashEffect;
    }
    [SerializeField] private GameObject _smashEffect;
}