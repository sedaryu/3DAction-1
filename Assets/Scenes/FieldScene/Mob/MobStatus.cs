using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobStatus : MonoBehaviour
{
    protected enum StateEnum //�L�����N�^�[�̏��
    {
        Normal, //�ʏ펞(Attack��Die�Ɉڍs�\)
        Damage, //�_���[�W��
        Groggy, //HitPoint��0�̎�(�Ƃǂ߂��h�����)
        Die //���S��(�ǂ̏�Ԃɂ��ڍs���Ȃ�)
    }
    protected StateEnum state = StateEnum.Normal; //�����l��Normal
    public bool IsMovable => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�
    public bool IsDamageble => (state == StateEnum.Damage);
    public bool IsReloadable => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�

    //�L�����N�^�[�̈ړ���NavMeshAgent�ōs��
    public NavMeshAgent Agent
    {
        get => _agent;
    }
    private NavMeshAgent _agent;

    //�A�j���[�^�[���i�[
    public Animator Animator
    {
        get => _animator;
    }
    private Animator _animator;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>(); //�G�[�W�F���g���擾
        _animator = GetComponent<Animator>(); //�A�j���[�^�[���擾
    }

    public void GoToDamageStateIfPossible() //��Ԃ�Damage�ɑJ�ڂ���
    {
        state = StateEnum.Damage;
    }

    public void GoToDieStateIfPossible() //��Ԃ�Die�ɑJ�ڂ���
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Die;
        Destroy(gameObject);
    }

    public void GoToNormalStateIfPossible() //��Ԃ�Normal�ɑJ�ڂ���
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Normal;
    }
}
