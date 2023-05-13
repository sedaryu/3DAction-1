using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour, ITargetable, IGrogable, IAttackable
{
    //ステータス
    private EnemyStatus status;
    //エフェクター
    private MobEffecter effecter;
    //ノックバッカー
    private EnemyKnockbacker knockbacker;

    //ITargetable
    public Transform Transform { get => transform; }
    //IAttackable
    public float Damage { get => status.EnemyParam.Attack; }

    //アクト
    private EnemyMove enemyMove;
    private EnemyDamage enemyDamage;
    private EnemyGroggy enemyGroggy;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
        effecter = GetComponent<MobEffecter>();
        knockbacker = GetComponent<EnemyKnockbacker>();

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

    public void Hit(Vector3 vector, float attack)
    {
        status.Damage(attack); //HitPointを減少させアニメーションを再生
        effecter.InstanceEffect("Hit"); //エフェクトを生成
        int hit = knockbacker.JudgeObstacle(transform, status.Agent.radius, vector * status.EnemyParam.Weight);
        for (int i = 0; i < hit; i++)
        {
            status.Damage(attack * 2f); //Hitした攻撃の二倍のダメージを追加で与える
            effecter.InstanceEffect("ObstacleHit"); //エフェクトも発生させる
        }
        knockbacker.Knockback(vector * status.EnemyParam.Weight); //ノックバック
    }

    public void Grog(SmashAct smash)
    { 
        Instantiate(smash, transform);
    }

    public void Attack()
    { 
        //アニメーションを再生
    }
}
