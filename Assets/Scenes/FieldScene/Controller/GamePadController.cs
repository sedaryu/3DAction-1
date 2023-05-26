using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadController : Controller
{
    void Update()
    {
        InputMoving();
        InputFiring();
        InputReloading();
        InputSmashing();
        InputLooking();
        InputBursting();

        onDecreasingAdrenaline.Invoke();
    }

    //�ړ��Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //�W���C�X�e�B�b�N����
        moving.x = Input.GetAxis("GPHorizontal"); //�������̈ړ����͂��擾
        moving.z = Input.GetAxis("GPVertical"); //�c�����̈ړ����͂��擾

        if (moving != null) onMoving?.Invoke(moving.normalized);
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
        Vector3 looking = new Vector3(0, 0, 0);

        //�W���C�X�e�B�b�N����
        looking.x = Input.GetAxis("GPRHorizontal"); //�������̈ړ����͂��擾
        looking.z = Input.GetAxis("GPRVertical"); //�c�����̈ړ����͂��擾

        if (looking != null) onLooking?.Invoke(looking);
    }

    //�A�h���i�����̃o�[�X�g�Ɋւ�����͂��󂯕t���郁�\�b�h
    public override void InputBursting()
    {
        if (Input.GetButtonDown("Burst")) onBursting?.Invoke();
    }
}
