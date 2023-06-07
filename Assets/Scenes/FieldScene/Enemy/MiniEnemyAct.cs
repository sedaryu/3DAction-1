using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyAct : EnemyAct
{
    protected override void OrderOutputMoving(Vector3 vector)
    {
        if (stater.State["Destroyable"]) return;
        if (!stater.State["Movable"]) mover.Move(vector, 0);
        else mover.Move(vector, parameter.Parameter("MoveSpeed"));
    }

    protected override void OrderOutputHealing()
    {
        if (stater.State["Smashable"]) return;
        parameter.SetParameter("HitPoint", parameter.Parameter("HealSpeed") * Time.deltaTime);
        hpCircleController.UpdateFill(parameter.PercentageParameter("HitPoint"));
    }

    protected override void OrderOutputHitting(Vector3 vector, float attack)
    {
        if (stater.State["Grogable"]) return;

        parameter.SetParameter("HitPoint", -attack); //HitPoint�����������A�j���[�V�������Đ�
        effecter.InstanceEffect("Hit"); //�G�t�F�N�g�𐶐�
        int hit = knockbacker.JudgeObstacle(transform, mover.Radius, vector * parameter.Parameter("Weight"));
        for (int i = 0; i < hit; i++)
        {
            parameter.SetParameter("HitPoint", -attack * 2); //Hit�����U���̓�{�̃_���[�W��ǉ��ŗ^����
            effecter.InstanceEffect("ObstacleHit"); //�G�t�F�N�g������������
        }
        knockbacker.Knockback(vector * parameter.Parameter("Weight")); //�m�b�N�o�b�N

        //animator.SetTrriger("Damage");
        hpCircleController.UpdateFill(parameter.PercentageParameter("HitPoint"));

        if (parameter.Parameter("HitPoint") <= 0)
        {
            stater.TransferState("Grogable", true);
            stater.TransferState("Movable", false);
            stater.TransferState("Attackable", false);
        }
    }

    protected override void OrderOutputCriticaling()
    {
        parameter.SetParameter("HitPoint", -500);
        effecter.InstanceEffect("ObstacleHit"); //�G�t�F�N�g������������
        stater.TransferState("Grogable", true);
        stater.TransferState("Movable", false);
        stater.TransferState("Attackable", false);
    }

    protected override bool IsGroggy()
    {
        return stater.State["Grogable"];
    }

    protected override bool IsDestroyed()
    {
        return stater.State["Destroyable"];
    }

    protected override void OrderOutputGrogging(Smash smash)
    {
        if (stater.State["Smashable"]) return;
        stater.TransferState("Grogable", false);
        stater.TransferState("Smashable", true);

        Smash smasherObject = Instantiate(smash, transform);
    }

    protected override void OrderOutputDying()
    {
        stater.TransferDestroyableState();
        mover.DisableAgent();
        for (int i = 0; i < transform.childCount; i++)
        { transform.GetChild(i).gameObject.SetActive(false); }
        Destroy(gameObject, 0.1f);
    }

    protected override string AttackKey()
    {
        return parameter.AttackKey;
    }

    protected override float OrderOutputAttacking()
    {
        if (!stater.State["Attackable"]) return 0;
        effecter.InstanceEffect("Hit"); //�G�t�F�N�g�𐶐�
        Destroy(gameObject, 0.02f);
        return parameter.Parameter("Attack");
    }

    protected override void OrderOutputSpawningItem()
    {
        
    }
}
