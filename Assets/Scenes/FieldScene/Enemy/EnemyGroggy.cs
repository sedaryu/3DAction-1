using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effecter;

public class EnemyGroggy
{
    //ステータス
    private EnemyParameter status;
    //エフェクター
    private MobEffecter effecter;

    public EnemyGroggy(EnemyParameter _status, MobEffecter _effecter)
    { 
        status = _status;
        effecter = _effecter;
    }

    public void Groggy(SmashParam smash)
    {
        status.GoToDieStateIfPossible(); //0以下ならば状態がDieに移行
        GameObject smashCollider = effecter.InstanceEffect(smash.SmashCollider); //SmashColliderを生成
        smashCollider.transform.parent = status.transform; //子オブジェクト化
        smashCollider.GetComponent<SmashAct>().StartTimer(smash.DestroyTime, smash.RangeMin);
    }
}
