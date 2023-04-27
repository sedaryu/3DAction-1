using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobStatus : MonoBehaviour
{
    protected enum StateEnum //キャラクターの状態
    {
        Normal, //通常時(AttackやDieに移行可能)
        Damage, //ダメージ時
        Groggy, //HitPointが0の時(とどめを刺される)
        Die //死亡時(どの状態にも移行しない)
    }
    protected StateEnum state = StateEnum.Normal; //初期値はNormal
    public bool IsMovable => (state == StateEnum.Normal); //状態がNormalであればtrueを返す
    public bool IsDamageble => (state == StateEnum.Damage);
    public bool IsReloadable => (state == StateEnum.Normal); //状態がNormalであればtrueを返す

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

    public void GoToDamageStateIfPossible() //状態がDamageに遷移する
    {
        state = StateEnum.Damage;
    }

    public void GoToDieStateIfPossible() //状態がDieに遷移する
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Die;
        Destroy(gameObject);
    }

    public void GoToNormalStateIfPossible() //状態がNormalに遷移する
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Normal;
    }
}
