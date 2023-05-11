using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Act
{
    public Action<Vector3> agentMove;
    public Action<string, float> animatorSetFloat;
    public Action<Quaternion> updateLookRotation;

    public void Move(bool noMove, Vector3 vector, float speed)
    {
        if (noMove) return;

        //移動入力を更新
        agentMove.Invoke(vector * speed * Time.deltaTime);
        //アニメーターに移動スピードを反映
        animatorSetFloat.Invoke("MoveSpeed", vector.magnitude);
        //キャラクターの向きを更新
        if (vector != Vector3.zero) updateLookRotation.Invoke(Quaternion.LookRotation(vector));
    }
}
