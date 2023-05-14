using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : MonoBehaviour
{
    //パラメーター
    public EnemyParam EnemyParam
    {
        get => _param;
    }
    private EnemyParam _param;

    //初期設定パラメーター
    [SerializeField] private EnemyParam initialParam;

    void Awake()
    {
        _param = new EnemyParam(initialParam);
    }

    //HitPointの自動回復を実行するメソッド
    public void RecoverDamage()
    {
        _param.HitPoint += EnemyParam.Recover * Time.deltaTime;
    }

    //被ダメージの際のHitPointの減少を実行するメソッド
    public void Damage(float damage)
    {
        _param.HitPoint -= damage;
    }
}
