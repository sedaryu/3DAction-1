using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// プレイヤーの入力を処理するクラス
/// </Summary>
public class KeyMouseController : Controller
{
    //バーチャルスティック
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
    }

    //移動に関する入力を受け付けるメソッド
    public override void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //ヴァーチャルスティック入力
        moving.x = virtualStick.Horizontal; //横方向の移動入力を取得
        moving.z = virtualStick.Vertical; //縦方向の移動入力を取得

        onMoving?.Invoke(moving);
    }

    //射撃に関する入力を受け付けるメソッド
    public override void InputFiring()
    {
        if (Input.GetButtonDown("Fire1")) onFiring?.Invoke();
    }

    //リロードに関する入力を受け付けるメソッド
    public override void InputReloading()
    {
        if (Input.GetButtonDown("Reload")) onReloading?.Invoke();
    }

    //スマッシュ攻撃に関する入力を受け付けるメソッド
    public override void InputSmashing()
    {
        if (Input.GetButtonDown("Smash")) onSmashing?.Invoke();
    }

    //方向転換に関する入力を受け付けるメソッド
    public override void InputLooking()
    {
        
    }
}
