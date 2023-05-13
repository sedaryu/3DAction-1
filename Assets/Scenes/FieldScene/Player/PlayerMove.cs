using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove
{
    //private PlayerStatus status;
    //private PlayerMover mover;
    //private PlayerAnimator animator;
    //private PlayerController controller;

    //public PlayerMove(PlayerStatus _status, PlayerMover _mover, PlayerAnimator _animator, PlayerController _controller)
    //{ 
    //    status = _status;
    //    mover = _mover;
    //    animator = _animator;
    //    controller = _controller;
    //}

    public void Move(PlayerParameter status, IMover mover, IAnimator animator, Controller controller)
    {
        if (status.IsNoMoveInvincible) return;

        float speed = mover.Move(controller.InputMoving(), status.PlayerParam.SpeedMax);
        animator.SetValue("MoveSpeed", speed);
    }
}
