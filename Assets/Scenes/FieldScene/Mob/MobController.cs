using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    //�p�����[�^�[
    [SerializeField] protected MobParam mobParam;
    //�X�e�[�^�X
    protected MobStatus mobStatus;
    //�L�����N�^�[�̈ړ���NavMeshAgent�ōs��
    protected NavMeshAgent agent;
    //�A�j���[�^�[���i�[
    protected Animator animator;

    protected void Awake()
    {
        mobStatus = new MobStatus();
        agent = GetComponent<NavMeshAgent>(); //�G�[�W�F���g���擾
        animator = GetComponent<Animator>(); //�A�j���[�^�[���擾
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�L�����N�^�[�̈ړ����Ǘ����郁�\�b�h
    protected void Move(Vector3 vector)
    {
        if (!mobStatus.IsMovable) return;

        agent.Move(vector * mobParam.Speed * Time.deltaTime); //�ړ����͂��X�V

        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2000 * Time.deltaTime);
        }
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        animator.SetFloat("MoveSpeed" , vector.magnitude);
    }
}
