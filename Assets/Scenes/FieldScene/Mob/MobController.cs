using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobController : MonoBehaviour
{
    //�L�����N�^�[�̈ړ���CharacterController�ōs��
    private CharacterController characterController;
    //�A�j���[�^�[���i�[
    private Animator animator;

    protected void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
        characterController.Move(vector * 5.5f * Time.deltaTime); //�ړ����͂��X�V
        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000 * Time.deltaTime);
        }
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        animator.SetFloat("MoveSpeed" , vector.magnitude);
    }
}
