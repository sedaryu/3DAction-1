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

    //移動に関する入力を受け付けるメソッド
    private void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);
        moving.x = Input.GetAxis("Horizontal"); //横方向の移動入力を取得
        moving.x = Input.GetButton("Horizontal") ? moving.x : 0;
        moving.z = Input.GetAxis("Vertical"); //縦方向の移動入力を取得
        moving.z = Input.GetButton("Vertical") ? moving.z : 0;
        Move(moving.normalized * 5.5f * Time.deltaTime); //入力を反映
    }
}
