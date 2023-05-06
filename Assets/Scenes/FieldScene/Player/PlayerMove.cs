using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //コントローラー
    private PlayerController controller;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(controller.InputMoving());
    }

    //キャラクターの移動を管理するメソッド
    private void Move(Vector3 vector)
    {
        if (status.IsNoMoveInvincible) return;

        status.Agent.Move(vector * status.PlayerParam.SpeedMax * Time.deltaTime); //移動入力を更新

        //キャラクターの向きを更新
        if (vector != Vector3.zero) transform.rotation = Quaternion.LookRotation(vector);
        //アニメーターに移動スピードを反映
        status.Animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
