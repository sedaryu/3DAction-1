using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : MonoBehaviour
{
    //�p�����[�^�[
    public EnemyParam EnemyParam
    {
        get => _param;
    }
    private EnemyParam _param;

    //�����ݒ�p�����[�^�[
    [SerializeField] private EnemyParam initialParam;

    void Awake()
    {
        _param = new EnemyParam(initialParam);
    }

    //HitPoint�̎����񕜂����s���郁�\�b�h
    public void RecoverDamage()
    {
        _param.HitPoint += EnemyParam.Recover * Time.deltaTime;
    }

    //��_���[�W�̍ۂ�HitPoint�̌��������s���郁�\�b�h
    public void Damage(float damage)
    {
        _param.HitPoint -= damage;
    }
}
