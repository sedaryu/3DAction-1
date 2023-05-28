using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAct : MonoBehaviour
{
    //パラメーター
    protected EnemyParameter parameter;
    //ステーター
    protected EnemyStater stater;
    //エネミームーバー
    protected EnemyMover mover;
    //エフェクター
    protected MobEffecter effecter;
    //ノックバッカー
    protected EnemyKnockbacker knockbacker;
    //アニメーター
    protected EnemyAnimator animator;

    //コントローラー
    protected EnemyController controller;
    //デスティネーター
    protected EnemyDestinater destinater;

    void Awake()
    {
        parameter = GetComponent<EnemyParameter>();
        stater = GetComponent<EnemyStater>();
        mover = GetComponent<EnemyMover>();
        effecter = GetComponent<MobEffecter>();
        knockbacker = GetComponent<EnemyKnockbacker>();
        animator = GetComponent<EnemyAnimator>();

        destinater = GetComponent<EnemyDestinater>();
        if (destinater!) destinater.onMoving += OrderOutputMoving;

        controller = GetComponent<EnemyController>();
        controller.onHealing += OrderOutputHealing;
        controller.onHitting += OrderOutputHitting;
        controller.onCriticaling += OrderOutputCriticaling;
        controller.isGroggy += IsGroggy;
        controller.onGrogging += OrderOutputGrogging;
        controller.onDying += OrderOutputDying;
        controller.attackKey += AttackKey;
        controller.onAttacking += OrderOutputAttacking;
        controller.onSpawningItem += OrderOutputSpawningItem;
    }

    protected abstract void OrderOutputMoving(Vector3 vector);

    protected abstract void OrderOutputHealing();

    protected abstract void OrderOutputHitting(Vector3 vector, float attack);

    protected abstract void OrderOutputCriticaling();

    protected abstract bool IsGroggy();

    protected abstract void OrderOutputGrogging(Smash smash);

    protected abstract void OrderOutputDying();

    protected abstract string AttackKey();

    protected abstract float OrderOutputAttacking();

    protected abstract void OrderOutputSpawningItem();
}
