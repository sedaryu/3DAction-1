using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <Summary>
/// キャラの状態(state)を管理し、
/// オブジェクトにアタッチされたEffecter・NavMeshAgent・Animatorを取得し格納する目的のクラス
/// </Summary>
public class MobStatus : MonoBehaviour
{
    protected enum StateEnum //キャラクターの状態
    {
        Normal, //通常時
        Invincible, //無敵時
        NoMoveInvincible, //移動不可無敵時
        Die //死亡時(どの状態にも移行しない)
    }
    protected StateEnum state = StateEnum.Normal; //初期値はNormal

    public bool IsNormal => (state == StateEnum.Normal); //状態がNormalであればtrueを返す
    public bool IsInvincible => (state == StateEnum.Invincible); //状態がInvincibleであればtrueを返す
    public bool IsNoMoveInvincible => (state == StateEnum.NoMoveInvincible); //状態がNoMoveInvincibleであればtrueを返す
    public bool IsDie => (state == StateEnum.Die); //状態がNoMoveInvincibleであればtrueを返す

    //エフェクト
    public Effecter Effecter
    {
        get => _effecter;
    }
    [SerializeField] private Effecter _effecter;

    //キャラクターの移動はNavMeshAgentで行う
    public NavMeshAgent Agent
    {
        get => _agent;
    }
    private NavMeshAgent _agent;

    //アニメーターを格納
    public Animator Animator
    {
        get => _animator;
    }
    private Animator _animator;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>(); //エージェントを取得
        _animator = GetComponent<Animator>(); //アニメーターを取得
    }

    public void GoToInvincibleStateIfPossible() //状態がInvincible(無敵)に遷移する
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Invincible;
    }

    public void GoToNoMoveInvincibleStateIfPossible() //状態がNoMoveInvincible(移動不可無敵)に遷移する
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.NoMoveInvincible;
    }

    public void GoToDieStateIfPossible() //状態がDieに遷移する
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Die;
    }

    public void GoToNormalStateIfPossible() //状態がNormalに遷移する
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Normal;
    }

    public virtual bool Damage(float damage)
    {
        return false;
    }
}
