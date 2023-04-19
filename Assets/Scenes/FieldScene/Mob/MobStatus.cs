using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    protected enum StateEnum //�L�����N�^�[�̏��
    {
        Normal, //�ʏ펞(Attack��Die�Ɉڍs�\)
        Attack, //�U����(���Ԍo�߂�Normal�Ɉڍs)
        Damage, //�_���[�W��
        Die //���S��(�ǂ̏�Ԃɂ��ڍs���Ȃ�)
    }

    protected StateEnum state = StateEnum.Normal; //�����l��Normal

    public bool IsMovable => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�
    public bool IsAttackable => (state == StateEnum.Normal); //��Ԃ�Normal�ł����true��Ԃ�

    protected Animator animator; //�A�j���[�^�[���i�[

    void Awake()
    { 
        animator = this.GetComponent<Animator>(); //�A�j���[�^�[���擾
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
