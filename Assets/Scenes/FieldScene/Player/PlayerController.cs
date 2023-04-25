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

        //���@�[�`�����X�e�B�b�N����
        moving.x = virtualStick.Horizontal; //�������̈ړ����͂��擾
        moving.z = virtualStick.Vertical; //�c�����̈ړ����͂���

        moving.x = Input.GetAxis("ASHorizontal");
        moving.z = Input.GetAxis("ASVertical");

        Move(moving); //���͂𔽉f
    }

    //�L�����N�^�[�̈ړ����Ǘ����郁�\�b�h
    protected void Move(Vector3 vector)
    {
        if (!status.IsMovable) return;

        agent.Move(vector * _param.Speed * Time.deltaTime); //�ړ����͂��X�V

        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero) transform.rotation = Quaternion.LookRotation(vector);
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
