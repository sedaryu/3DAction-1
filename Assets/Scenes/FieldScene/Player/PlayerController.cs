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

    //移動に関する入力を受け付けるメソッド
    private void InputMoving()
    {
        Vector3 moving = new Vector3(0, 0, 0);

        //キー入力
        moving.x = Input.GetButton("Horizontal") ? Input.GetAxis("Horizontal") : 0; //横方向の移動入力を取得
        moving.z = Input.GetButton("Vertical") ? Input.GetAxis("Vertical") : 0; //縦方向の移動入力を取得

        //アナログスティック入力
        moving.x = Input.GetAxis("ASHorizontal") == 0 ? moving.x : Input.GetAxis("ASHorizontal"); //横方向の移動入力を取得
        moving.z = Input.GetAxis("ASVertical") == 0 ? moving.z : Input.GetAxis("ASVertical"); //縦方向の移動入力を取得

        Move(moving.normalized); //入力を反映
    }
}
