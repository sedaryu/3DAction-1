using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MobController
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        moving.x = Input.GetAxis("Horizontal"); //�������̈ړ����͂��擾
        moving.x = Input.GetButton("Horizontal") ? moving.x : 0;
        moving.z = Input.GetAxis("Vertical"); //�c�����̈ړ����͂��擾
        moving.z = Input.GetButton("Vertical") ? moving.z : 0;
        Move(moving.normalized * 5.5f * Time.deltaTime); //���͂𔽉f
    }
}
