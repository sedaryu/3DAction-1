using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MobStatus
{
    //パラメーター
    public EnemyParam Param
    {
        get => _param;
    }
    private EnemyParam _param;

    //初期設定パラメーター
    [SerializeField] private EnemyParam initialParam;

    protected override void Awake()
    {
        base.Awake();
        _param = new EnemyParam(initialParam);
    }

    // Start is called before the first frame update
    void Start()
    {
        Agent.speed = Random.Range(Param.SpeedMin, Param.SpeedMax); //param.Speed; //パラメーターからスピードを取得
    }

    // Update is called once per frame
    void Update()
    {
        RecoverDamage();
    }

    public void Damage(float damage)
    {
        _param.HitPoint -= damage;
        if (Param.HitPoint <= 0) GoToDieStateIfPossible();
    }

    public void RecoverDamage()
    {
        _param.HitPoint += Param.Recover * Time.deltaTime;
    }
}
