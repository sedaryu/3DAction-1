using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //コントローラー
    private PlayerController controller;

    //アクト
    private PlayerMove playerMove;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();

        playerMove = new PlayerMove(status);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.Move(controller.InputMoving());
    }
}
