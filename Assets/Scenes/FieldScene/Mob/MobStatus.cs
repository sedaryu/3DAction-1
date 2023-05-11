using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <Summary>
/// �L�����̏��(state)���Ǘ����A
/// �I�u�W�F�N�g�ɃA�^�b�`���ꂽNavMeshAgent�EAnimator���擾���i�[����ړI�̃N���X
/// </Summary>
public class MobStatus : MonoBehaviour
{
    protected enum StateEnum //�L�����N�^�[�̏��
    {
        Normal, //�ʏ펞
        Invincible, //���G��
        NoMoveInvincible, //�ړ��s���G��
        Die //���S��(�ǂ̏�Ԃɂ��ڍs���Ȃ�)
    }
    protected StateEnum state = StateEnum.Normal; //�����l��Normal

    public bool IsNormal => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�
    public bool IsInvincible => (state == StateEnum.Invincible); //��Ԃ�Invincible�ł����true��Ԃ�
    public bool IsNoMoveInvincible => (state == StateEnum.NoMoveInvincible); //��Ԃ�NoMoveInvincible�ł����true��Ԃ�
    public bool IsDie => (state == StateEnum.Die); //��Ԃ�NoMoveInvincible�ł����true��Ԃ�

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

    public void GoToInvincibleStateIfPossible() //��Ԃ�Invincible(���G)�ɑJ�ڂ���
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Invincible;
    }

    public void GoToNoMoveInvincibleStateIfPossible() //��Ԃ�NoMoveInvincible(�ړ��s���G)�ɑJ�ڂ���
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.NoMoveInvincible;
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

    public bool IsNormalMethod() { if (state == StateEnum.Normal) { return true; } else { return false; } }
}
