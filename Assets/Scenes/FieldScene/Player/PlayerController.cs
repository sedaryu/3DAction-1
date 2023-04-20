using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MobController
{
    [SerializeField] private GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputMoving();

        if (Input.GetButtonDown("Fire1"))
        { 
            Instantiate(projectilePrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        }
    }

    //�ړ��Ɋւ�����͂��󂯕t���郁�\�b�h
    private void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //�L�[����
        moving.x = Input.GetButton("Horizontal") ? Input.GetAxis("Horizontal") : 0; //�������̈ړ����͂��擾
        moving.z = Input.GetButton("Vertical") ? Input.GetAxis("Vertical") : 0; //�c�����̈ړ����͂��擾

        //�A�i���O�X�e�B�b�N����
        moving.x = Input.GetAxis("ASHorizontal") == 0 ? moving.x : Input.GetAxis("ASHorizontal"); //�������̈ړ����͂��擾
        moving.z = Input.GetAxis("ASVertical") == 0 ? moving.z : Input.GetAxis("ASVertical"); //�c�����̈ړ����͂��擾

        Move(moving.normalized); //���͂𔽉f
    }
}
