using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{
    //ステータス
    private EnemyStatus status;
    //エフェクター
    private MobEffecter effecter;
    //レファレンシッド
    private EnemyReferenced referenced;

    //アクト
    private EnemyMove enemyMove;
    private EnemyDamage enemyDamage;
    private EnemyGroggy enemyGroggy;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
        effecter = GetComponent<MobEffecter>();
        referenced = GetComponent<EnemyReferenced>();

        //移動
        enemyMove = new EnemyMove(status);
        //ダメージ
        enemyDamage = new EnemyDamage(status, effecter);
        //グロッキー
        enemyGroggy = new EnemyGroggy(status, effecter);

        //外部から発生されるメソッドを設定
        referenced.onTriggerAttacked += enemyDamage.Hit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //移動してプレイヤーを追跡する
        enemyMove.Move();
    }
}
