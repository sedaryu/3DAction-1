using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAct : MonoBehaviour, ITargetable, IGrogable, IAttackable
{
    //パラメーター
    private EnemyParameter parameter;
    //ステーター
    private EnemyStater stater;
    //エネミームーバー
    private EnemyMover mover;
    //エフェクター
    private MobEffecter effecter;
    //ノックバッカー
    private EnemyKnockbacker knockbacker;
    //アニメーター
    private EnemyAnimator animator;

    //IGrogable
    public bool Groggy { get => stater.State["Grogable"]; }

    //デスティネーター
    private EnemyDestinater destinater;

    void Awake()
    {
        parameter = GetComponent<EnemyParameter>();
        stater = GetComponent<EnemyStater>();
        mover = GetComponent<EnemyMover>();
        effecter = GetComponent<MobEffecter>();
        knockbacker = GetComponent<EnemyKnockbacker>();
        animator = GetComponent<EnemyAnimator>();

        destinater = GetComponent<EnemyDestinater>();
        destinater.onMoving += OrderOutputMoving;
    }

    private void OrderOutputMoving(Vector3 vector)
    {
        if (!stater.State["Movable"]) mover.Move(vector, 0);
        else mover.Move(vector, parameter.EnemyParam.SpeedMax);
    }

    public void Hit(Vector3 vector, float attack)
    {
        if (stater.State["Grogable"]) return;

        parameter.Damage(attack); //HitPointを減少させアニメーションを再生
        effecter.InstanceEffect("Hit"); //エフェクトを生成
        int hit = knockbacker.JudgeObstacle(transform, mover.Radius, vector * parameter.EnemyParam.Weight);
        for (int i = 0; i < hit; i++)
        {
            parameter.Damage(attack * 2f); //Hitした攻撃の二倍のダメージを追加で与える
            effecter.InstanceEffect("ObstacleHit"); //エフェクトも発生させる
        }
        knockbacker.Knockback(vector * parameter.EnemyParam.Weight); //ノックバック

        animator.SetTrriger("Damage");

        if (parameter.EnemyParam.HitPoint <= 0)
        {
            stater.TransferState("Grogable", true);
            stater.TransferState("Movable", false);
            stater.TransferState("Attackable", false);
        }
    }

    public void Grog(Smasher smash, float time)
    {
        if (stater.State["Smashable"]) return;
        stater.TransferState("Grogable", false);
        stater.TransferState("Smashable", true);

        Smasher smasherObject = Instantiate(smash, transform);
        smasherObject.StartTimer(time);
    }

    public float Attack()
    {
        if (!stater.State["Attackable"]) return 0;
        return parameter.EnemyParam.Attack;
    }
}
