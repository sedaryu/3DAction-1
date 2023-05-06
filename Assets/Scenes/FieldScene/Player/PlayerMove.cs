using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�X�e�[�^�X
    private PlayerStatus status;
    //�R���g���[���[
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

    //�L�����N�^�[�̈ړ����Ǘ����郁�\�b�h
    private void Move(Vector3 vector)
    {
        if (status.IsNoMoveInvincible) return;

        status.Agent.Move(vector * status.PlayerParam.SpeedMax * Time.deltaTime); //�ړ����͂��X�V

        //�L�����N�^�[�̌������X�V
        if (vector != Vector3.zero) transform.rotation = Quaternion.LookRotation(vector);
        //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        status.Animator.SetFloat("MoveSpeed", vector.magnitude);
    }
}
