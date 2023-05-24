using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// �v���C���[�̓��͂���������N���X
/// </Summary>
public class KeyMouseController : Controller
{
    //�o�[�`�����X�e�B�b�N
    private Joystick virtualStick;

    void Awake()
    {
        virtualStick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
    }

    void Update()
    {
        InputMoving();
        InputFiring();
        InputReloading();
        InputSmashing();
        InputBursting();

        onDecreasingAdrenaline.Invoke();
    }

    //�ړ��Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //���@�[�`�����X�e�B�b�N����
        moving.x = virtualStick.Horizontal; //�������̈ړ����͂��擾
        moving.z = virtualStick.Vertical; //�c�����̈ړ����͂��擾

        onMoving?.Invoke(moving);
    }

    //�ˌ��Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputFiring()
    {
        if (Input.GetButtonDown("Fire1")) onFiring?.Invoke();
    }

    //�����[�h�Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputReloading()
    {
        if (Input.GetButtonDown("Reload")) onReloading?.Invoke();
    }

    //�X�}�b�V���U���Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputSmashing()
    {
        if (Input.GetButtonDown("Smash")) onSmashing?.Invoke();
    }

    //�����]���Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputLooking()
    {
        
    }

    //�A�h���i�����̃o�[�X�g�Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputBursting()
    {
        if (Input.GetButtonDown("Burst")) onBursting?.Invoke();
    }
}
