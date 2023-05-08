using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    private PlayerStatus status;

    public PlayerMove(PlayerStatus _status)
    {
        status = _status;
    }

    public void Move(Vector3 vector)
    {
        if (status.IsNoMoveInvincible) return;

        status.Agent.Move(vector * status.PlayerParam.SpeedMax * Time.deltaTime); //移動入力を更新

        //キャラクターの向きを更新
        if (vector != Vector3.zero) status.transform.rotation = Quaternion.LookRotation(vector);
        //アニメーターに移動スピードを反映
        status.Animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
