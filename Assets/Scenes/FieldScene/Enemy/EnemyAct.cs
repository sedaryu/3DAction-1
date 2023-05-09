using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour
{
    //ステータス
    private EnemyStatus status;
    //エフェクター
    private MobEffecter effecter;

    //アクト
    private EnemyMove enemyMove;
    private EnemyDamage enemyDamage;
    private EnemyGroggy enemyGroggy;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
        effecter = GetComponent<MobEffecter>();

        //移動
        enemyMove = new EnemyMove(status);
        //ダメージ
        enemyDamage = new EnemyDamage(status, effecter);
        //グロッキー
        enemyGroggy = new EnemyGroggy(status, effecter);
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

    public void Hit(Vector3 vector, float attack, SmashParam smash)
    {
        if (enemyDamage.Hit(vector, attack)) enemyGroggy.Groggy(smash);
    }
}
