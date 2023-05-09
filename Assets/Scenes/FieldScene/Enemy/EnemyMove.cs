using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyMove 
{
    //ステータス
    private EnemyStatus status;
    //追跡するプレイヤー
    private Transform player;

    public EnemyMove(EnemyStatus _status)
    {
        status = _status;
        player = GameObject.Find("Player").GetComponent<Transform>(); //プレイヤーの位置を取得
    }

    public void Move()
    {
        if (status.IsNormal && player != null) status.Agent.destination = player.position; //プレイヤーを追跡
        else status.Agent.speed = 0;

        status.Animator.SetFloat("MoveSpeed", status.Agent.velocity.magnitude); //アニメーターに移動スピードを反映
    }
}
