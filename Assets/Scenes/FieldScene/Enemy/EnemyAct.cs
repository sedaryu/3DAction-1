using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAct : MonoBehaviour
{
    //�p�����[�^�[
    protected EnemyParameter parameter;
    //�X�e�[�^�[
    protected EnemyStater stater;
    //�G�l�~�[���[�o�[
    protected EnemyMover mover;
    //�G�t�F�N�^�[
    protected MobEffecter effecter;
    //�m�b�N�o�b�J�[
    protected EnemyKnockbacker knockbacker;
    //�A�j���[�^�[
    protected EnemyAnimator animator;

    //�R���g���[���[
    protected EnemyController controller;
    //�f�X�e�B�l�[�^�[
    protected EnemyDestinater destinater;

    //UI
    protected FilledCircleController hpCircleController;

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
        controller.isDestroyed += IsDestroyed;
        controller.onGrogging += OrderOutputGrogging;
        controller.onDying += OrderOutputDying;
        controller.attackKey += AttackKey;
        controller.onAttacking += OrderOutputAttacking;
        controller.onSpawningItem += OrderOutputSpawningItem;

        hpCircleController = transform.Find("HitPointCircle").gameObject.GetComponent<FilledCircleController>();
    }

    protected abstract void OrderOutputMoving(Vector3 vector);

    protected abstract void OrderOutputHealing();

    protected abstract void OrderOutputHitting(Vector3 vector, float attack);

    protected abstract void OrderOutputCriticaling();

    protected abstract bool IsGroggy();

    protected abstract bool IsDestroyed();

    protected abstract void OrderOutputGrogging(Smash smash);

    protected abstract void OrderOutputDying();

    protected abstract string AttackKey();

    protected abstract float OrderOutputAttacking();

    protected abstract void OrderOutputSpawningItem();
}
