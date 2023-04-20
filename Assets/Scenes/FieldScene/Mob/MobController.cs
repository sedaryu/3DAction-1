using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MobController : MonoBehaviour
{
    //�L�����N�^�[�̈ړ���NavMeshAgent�ōs��
    protected NavMeshAgent agent;
    //�A�j���[�^�[���i�[
    private Animator animator;

    protected void Awake()
    {
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
        agent.Move(vector * 5.5f * Time.deltaTime); //�ړ����͂��X�V

        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000 * Time.deltaTime);
        }
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        animator.SetFloat("MoveSpeed" , vector.magnitude);
    }

    protected bool JudgeGrounded() //�ڒn���菈�����s��
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        Debug.DrawRay(this.transform.position, Vector3.down * 0.1f, Color.red);

        if (Physics.Raycast(ray, 0.1f, 1 << 6))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
