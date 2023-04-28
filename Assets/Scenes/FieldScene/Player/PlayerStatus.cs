using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MobStatus
{
    //パラメーター
    public PlayerParam Param
    {
        get => _param;
    }
    private PlayerParam _param;

    //初期設定パラメーター
    [SerializeField] private PlayerParam initialParam;

    protected override void Awake()
    {
        base.Awake();
        _param = new PlayerParam(initialParam);
    }

    //被ダメージの際のHitPointの減少を実行するメソッド
    public override void Damage(float damage)
    {
        _param.HitPoint -= damage;
        if (Param.HitPoint <= 0) GoToDieStateIfPossible(); //0以下ならば状態がDieに移行
        Debug.Log(Param.HitPoint);
    }
}
