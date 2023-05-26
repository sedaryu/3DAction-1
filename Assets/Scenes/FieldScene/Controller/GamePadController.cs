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

    //移動に関する入力を受け付けるメソッド
    public override void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //ジョイスティック入力
        moving.x = Input.GetAxis("GPHorizontal"); //横方向の移動入力を取得
        moving.z = Input.GetAxis("GPVertical"); //縦方向の移動入力を取得

        if (moving != null) onMoving?.Invoke(moving.normalized);
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
        Vector3 looking = new Vector3(0, 0, 0);

        //ジョイスティック入力
        looking.x = Input.GetAxis("GPRHorizontal"); //横方向の移動入力を取得
        looking.z = Input.GetAxis("GPRVertical"); //縦方向の移動入力を取得

        if (looking != null) onLooking?.Invoke(looking);
    }

    //アドレナリンのバーストに関する入力を受け付けるメソッド
    public override void InputBursting()
    {
        if (Input.GetButtonDown("Burst")) onBursting?.Invoke();
    }
}
