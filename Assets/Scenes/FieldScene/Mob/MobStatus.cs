using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobStatus : MonoBehaviour
{
    protected enum StateEnum //�L�����N�^�[�̏��
    {
        Normal, //�ʏ펞(Attack��Die�Ɉڍs�\)
        Invincible, //���G��
        NoMoveInvincible, //�ړ��s���G
        Die //���S��(�ǂ̏�Ԃɂ��ڍs���Ȃ�)
    }
    protected StateEnum state = StateEnum.Normal; //�����l��Normal

    public bool IsMovable => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�

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

    public void GoToInvincibleStateIfPossible() //��Ԃ�Damage�ɑJ�ڂ���
    {
        state = StateEnum.Invincible;
    }

    public void GoToDieStateIfPossible() //��Ԃ�Die�ɑJ�ڂ���
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Die;
    }

    public void GoToNormalStateIfPossible() //��Ԃ�Normal�ɑJ�ڂ���
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Normal;
    }

    public virtual void Damage(float damage)
    {
    }
}
