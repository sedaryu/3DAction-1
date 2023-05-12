using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// �v���C���[�̓��͂���������N���X
/// </Summary>
public class PlayerController : MonoBehaviour, IController
{
    //�o�[�`�����X�e�B�b�N
    private Joystick virtualStick;

    void Awake()
    {
        virtualStick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
    }

    //�ړ��Ɋւ�����͂��󂯕t���郁�\�b�h
    public Vector3 InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //���@�[�`�����X�e�B�b�N����
        moving.x = virtualStick.Horizontal; //�������̈ړ����͂��擾
        moving.z = virtualStick.Vertical; //�c�����̈ړ����͂���
        return moving; //���͂𔽉f
    }

    //�ˌ��Ɋւ�����͂��󂯕t���郁�\�b�h
    public bool InputFiring()
    {
        if (Input.GetButtonDown("Fire1")) return true;

        return false;
    }

    //�����[�h�Ɋւ�����͂��󂯕t���郁�\�b�h
    public bool InputReloading()
    {
        if (Input.GetButtonDown("Reload")) return true;

        return false;
    }

    //�X�}�b�V���U���Ɋւ�����͂��󂯕t���郁�\�b�h
    public bool InputSmashing()
    {
        if (Input.GetButtonDown("Smash")) return true;

        return false;
    }
}
