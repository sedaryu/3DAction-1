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

        //�ړ����͂��X�V
        agentMove.Invoke(vector * speed * Time.deltaTime);
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        animatorSetFloat.Invoke("MoveSpeed", vector.magnitude);
        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero) updateLookRotation.Invoke(Quaternion.LookRotation(vector));
    }
}
