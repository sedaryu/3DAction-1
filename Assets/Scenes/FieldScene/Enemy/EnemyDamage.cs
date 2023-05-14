using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�L�����N�^�[�ɍU�����q�b�g�����ۂ́A�_���[�W�܂��m�b�N�o�b�N�����A
/// �����ɏՓ˔�����s���ړI�̃N���X
/// </summary>
public class EnemyDamage
{
    //�X�e�[�^�X
    private EnemyParameter status;
    //�G�t�F�N�^�[
    private MobEffecter effecter;

    public EnemyDamage(EnemyParameter _status, MobEffecter _effecter)
    { 
        status = _status;
        effecter = _effecter;
    }

    /// <summary>
    /// �v���C���[�̍U�����q�b�g�����ۂ́A�m�b�N�o�b�N�A�_���[�W�A�Փ˔���A�A�j���[�V�����A�G�t�F�N�g������
    /// �U���̌���HitPoint��0�ȉ��ɂȂ����ꍇ�́A�߂�l�Ƃ���true��Ԃ��A����ȊO��false��Ԃ�
    /// </summary>
    /// <param name="vector">�U�����q�b�g�����ۂ̃m�b�N�o�b�N�����Ƌ���</param>
    /// <param name="attack">�U�����q�b�g�����ێ󂯂�_���[�W��</param>
    /// <returns>�U���̌���HitPoint��0�ȉ��ɂȂ����ꍇ�Atrue��Ԃ��A����ȊO��false</returns>
    public void Hit(Vector3 vector, float attack)
    {
        status.transform.rotation = Quaternion.LookRotation(-vector.normalized); //�v���C���[�̕���������

        status.Animator.SetTrigger("Damage"); //��_���[�W�̍ۂ̃A�j���[�V���������s
        JudgeObstacle(vector, attack); //�Փ˔�������s
        effecter.InstanceEffect("Hit"); //�G�t�F�N�g�𐶐�
        Knockback(vector); //�m�b�N�o�b�N�����s
        status.Damage(attack); //HitPoint������������
    }

    /// <summary>
    /// ������vector��Weight�̃p�����[�^�[���|�����Ԃ�m�b�N�o�b�N������
    /// </summary>
    /// <param name="vector">�U�����q�b�g�����ۃm�b�N�o�b�N��������Ƌ���</param>
    public void Knockback(Vector3 vector)
    {
        status.transform.Translate(vector * status.EnemyParam.Weight, Space.World); //��ѓ���̕����Ƀm�b�N�o�b�N
    }

    /// <summary>
    /// �m�b�N�o�b�N������Obstacle�I�u�W�F�N�g�����݂��邩Ray�𓊎˂��Ĕ��肵�A
    /// ���݂����ꍇ�A�Փ˃_���[�W��^����ړI�̃��\�b�h
    /// </summary>
    /// <param name="vector">�U�����q�b�g�����ۃm�b�N�o�b�N��������Ƌ���</param>
    /// <param name="attack">�q�b�g�����U���̃_���[�W</param>
    private void JudgeObstacle(Vector3 vector, float attack)
    {
        Vector3 avoidSpace = status.transform.forward * -0.1f * status.Agent.radius; //������Ray��������Ȃ��悤�Ɍ��Ԃ����
        //�L�����̌���ɁAX���ɐ�����Ray�𔼌a�ܕ��������Ԋu�œ���
        Ray[] rays = new Ray[5]
        {
            new Ray(status.transform.position + status.transform.right * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.right * -0.5f * status.Agent.radius + status.transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.forward * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.right * 0.5f * status.Agent.radius + status.transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(status.transform.position + status.transform.right * status.Agent.radius + avoidSpace, vector.normalized),
        };

        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector * status.EnemyParam.Weight, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, (vector * status.EnemyParam.Weight).magnitude - avoidSpace.magnitude, 1 << 8))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    status.Damage(attack * 2f); //Hit�����U���̓�{�̃_���[�W��ǉ��ŗ^����
                    effecter.InstanceEffect("ObstacleHit"); //�G�t�F�N�g������������
                }
            }
        }
    }
}
