using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MobController
{
    //バーチャルスティック
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

    //移動に関する入力を受け付けるメソッド
    private void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //キー入力
        moving.x = Input.GetButton("Horizontal") ? Input.GetAxis("Horizontal") : 0; //横方向の移動入力を取得
        moving.z = Input.GetButton("Vertical") ? Input.GetAxis("Vertical") : 0; //縦方向の移動入力を取得
        moving.x = virtualStick.Horizontal;
        moving.z = virtualStick.Vertical;

        //アナログスティック入力
        moving.x = Input.GetAxis("ASHorizontal") == 0 ? moving.x : Input.GetAxis("ASHorizontal"); //横方向の移動入力を取得
        moving.z = Input.GetAxis("ASVertical") == 0 ? moving.z : Input.GetAxis("ASVertical"); //縦方向の移動入力を取得

        Move(moving.normalized); //入力を反映
    }

    //キャラクターの移動を管理するメソッド
    protected void Move(Vector3 vector)
    {
        if (!status.IsMovable) return;

        agent.Move(vector * _param.Speed * Time.deltaTime); //移動入力を更新

        //キャラクターの向きを更新
        if (vector != Vector3.zero) transform.rotation = Quaternion.LookRotation(vector);
        //if (vector != Vector3.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(vector);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2000 * Time.deltaTime);
        //}
        //アニメーターに移動スピードを反映
        animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
