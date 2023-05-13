using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MobStatus
{
    //パラメーター
    public EnemyParam EnemyParam
    {
        get => _param;
    }
    private EnemyParam _param;

    //初期設定パラメーター
    [SerializeField] private EnemyParam initialParam;

    public bool IsSmashable => (state == StateEnum.Die); //状態がDieであればtrueを返す

    protected override void Awake()
    {
        base.Awake();
        _param = new EnemyParam(initialParam);
    }

    // Start is called before the first frame update
    void Start()
    {
        Agent.speed = Random.Range(EnemyParam.SpeedMin, EnemyParam.SpeedMax); //パラメーターからスピードを取得
    }

    // Update is called once per frame
    void Update()
    {
        //HitPointの自動回復
        if (state == StateEnum.Normal) RecoverDamage();
    }

    //HitPointの自動回復を実行するメソッド
    public void RecoverDamage()
    {
        _param.HitPoint += EnemyParam.Recover * Time.deltaTime;
    }

    //被ダメージの際のHitPointの減少を実行するメソッド
    public override void Damage(float damage)
    {
        _param.HitPoint -= damage;
        Animator.SetTrigger("Damage"); //被ダメージの際のアニメーションを実行
        if (EnemyParam.HitPoint <= 0)
        {
            GoToDieStateIfPossible();
        }
    }
}
