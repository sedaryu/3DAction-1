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

        //ヴァーチャルスティック入力
        moving.x = virtualStick.Horizontal; //横方向の移動入力を取得
        moving.z = virtualStick.Vertical; //縦方向の移動入力を取

        moving.x = Input.GetAxis("ASHorizontal");
        moving.z = Input.GetAxis("ASVertical");

        Move(moving); //入力を反映
    }

    //キャラクターの移動を管理するメソッド
    protected void Move(Vector3 vector)
    {
        if (!status.IsMovable) return;

        agent.Move(vector * _param.Speed * Time.deltaTime); //移動入力を更新

        //キャラクターの向きを更新
        if (vector != Vector3.zero) transform.rotation = Quaternion.LookRotation(vector);
        //アニメーターに移動スピードを反映
        animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
