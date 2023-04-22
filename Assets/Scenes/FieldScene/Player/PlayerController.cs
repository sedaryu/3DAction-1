using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MobController
{
    //�o�[�`�����X�e�B�b�N
    private Joystick virtualStick;

    // Start is called before the first frame update
    void Start()
    {
        virtualStick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMoving();
    }

    //�ړ��Ɋւ�����͂��󂯕t���郁�\�b�h
    private void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //�L�[����
        moving.x = Input.GetButton("Horizontal") ? Input.GetAxis("Horizontal") : 0; //�������̈ړ����͂��擾
        moving.z = Input.GetButton("Vertical") ? Input.GetAxis("Vertical") : 0; //�c�����̈ړ����͂��擾
        moving.x = virtualStick.Horizontal;
        moving.z = virtualStick.Vertical;

        //�A�i���O�X�e�B�b�N����
        moving.x = Input.GetAxis("ASHorizontal") == 0 ? moving.x : Input.GetAxis("ASHorizontal"); //�������̈ړ����͂��擾
        moving.z = Input.GetAxis("ASVertical") == 0 ? moving.z : Input.GetAxis("ASVertical"); //�c�����̈ړ����͂��擾

        Move(moving.normalized); //���͂𔽉f
    }

    //�L�����N�^�[�̈ړ����Ǘ����郁�\�b�h
    protected void Move(Vector3 vector)
    {
        if (!status.IsMovable) return;

        agent.Move(vector * _param.Speed * Time.deltaTime); //�ړ����͂��X�V

        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero) transform.rotation = Quaternion.LookRotation(vector);
        //if (vector != Vector3.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(vector);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2000 * Time.deltaTime);
        //}
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
