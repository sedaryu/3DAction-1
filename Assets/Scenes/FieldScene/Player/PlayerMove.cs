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

        status.Agent.Move(vector * status.PlayerParam.SpeedMax * Time.deltaTime); //�ړ����͂��X�V

        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero) status.transform.rotation = Quaternion.LookRotation(vector);
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        status.Animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
