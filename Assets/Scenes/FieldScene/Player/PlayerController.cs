using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MobController
{
    //バーチャルスティック
    private Joystick virtualStick;

    void Awake()
    {
        virtualStick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
    }

    //移動に関する入力を受け付けるメソッド
    public Vector3 InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //ヴァーチャルスティック入力
        moving.x = virtualStick.Horizontal; //横方向の移動入力を取得
        moving.z = virtualStick.Vertical; //縦方向の移動入力を取
        return moving; //入力を反映
    }

    public bool InputFiring()
    {
        if (Input.GetButtonDown("Fire1")) return true;

        return false;
    }

    public bool InputReloading()
    {
        if (Input.GetButtonDown("Reload")) return true;

        return false;
    }

    public bool InputCombating()
    {
        if (Input.GetButtonDown("Combat")) return true;

        return false;
    }
}
