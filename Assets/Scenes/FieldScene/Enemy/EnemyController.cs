using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MobController
{
    //ステータス
    private EnemyParameter status;
    //追跡するプレイヤー
    private Transform player;

    void Awake()
    {
        status = GetComponent<EnemyParameter>();
        player = GameObject.Find("Player").GetComponent<Transform>(); //プレイヤーの位置を取得
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsNormal) status.Agent.destination = player.position; //プレイヤーを追跡
        status.Animator.SetFloat("MoveSpeed", status.Agent.velocity.magnitude); //アニメーターに移動スピードを反映
    }
}
