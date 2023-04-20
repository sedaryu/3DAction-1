using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatus
{
    protected enum StateEnum //キャラクターの状態
    {
        Normal, //通常時(AttackやDieに移行可能)
        Attack, //攻撃時(時間経過でNormalに移行)
        Damage, //ダメージ時
        Die //死亡時(どの状態にも移行しない)
    }

    protected StateEnum state = StateEnum.Normal; //初期値はNormal

    public bool IsMovable => (state == StateEnum.Normal); //状態がNormalであればtrueを返す
    public bool IsAttackable => (state == StateEnum.Normal); //状態がNormalであればtrueを返す
}
