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
        Agent.speed = Random.Range(0.5f, 4.0f); //param.Speed; //�p�����[�^�[����X�s�[�h���擾
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        _param.HitPoint -= damage;
        if(Param.HitPoint == 0) gameObject.SetActive(false);
    }
}
