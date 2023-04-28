using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MobStatus
{
    //�p�����[�^�[
    public EnemyParam Param
    {
        get => _param;
    }
    private EnemyParam _param;

    //�����ݒ�p�����[�^�[
    [SerializeField] private EnemyParam initialParam;

    protected override void Awake()
    {
        base.Awake();
        _param = new EnemyParam(initialParam);
    }

    // Start is called before the first frame update
    void Start()
    {
        Agent.speed = Random.Range(Param.SpeedMin, Param.SpeedMax); //�p�����[�^�[����X�s�[�h���擾
    }

    // Update is called once per frame
    void Update()
    {
        RecoverDamage(); //HitPoint�̎�����
    }

    //HitPoint�̎����񕜂����s���郁�\�b�h
    public void RecoverDamage()
    {
        _param.HitPoint += Param.Recover * Time.deltaTime;
    }

    //��_���[�W�̍ۂ�HitPoint�̌��������s���郁�\�b�h
    public override void Damage(float damage)
    {
        _param.HitPoint -= damage;
        if (Param.HitPoint <= 0) GoToDieStateIfPossible(); //0�ȉ��Ȃ�Ώ�Ԃ�Die�Ɉڍs
    }
}
