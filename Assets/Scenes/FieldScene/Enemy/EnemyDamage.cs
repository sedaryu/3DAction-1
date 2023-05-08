using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�L�����N�^�[�ɍU�����q�b�g�����ۂ́A�_���[�W�܂��m�b�N�o�b�N�����A
/// �����ɏՓ˔�����s���ړI�̃N���X
/// </summary>
public class EnemyDamage : MonoBehaviour
{
    //�X�e�[�^�X
    private EnemyStatus status;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
    }

    /// <summary>
    /// �v���C���[�̍U�����q�b�g�����ۂ́A�m�b�N�o�b�N�A�_���[�W�A�Փ˔���A�A�j���[�V�����A�G�t�F�N�g������
    /// </summary>
    /// <param name="vector">�U�����q�b�g�����ۂ̃m�b�N�o�b�N�����Ƌ���</param>
    /// <param name="attack">�U�����q�b�g�����ێ󂯂�_���[�W��</param>
    /// <param name="smash">�U���̌���HitPoint��0�ɂȂ����ہA�o��������SmashCollider�̃p�����[�^�[���i�[</param>
    public void Hit(Vector3 vector, float attack, SmashParam smash)
    {
        transform.rotation = Quaternion.LookRotation(-vector.normalized); //�v���C���[�̕���������

        status.Animator.SetTrigger("Damage"); //��_���[�W�̍ۂ̃A�j���[�V���������s
        JudgeObstacle(vector, attack); //�Փ˔�������s
        Instantiate(status.Effecter.GetEffectFromKey("Hit"), transform.position, transform.rotation); //�G�t�F�N�g�𐶐�
        Knockback(vector); //�m�b�N�o�b�N�����s
        if (!status.IsSmashable && status.Damage(attack)) //�U���̌���HitPoint��0�ɂȂ����ۂ̏���
        {
            status.GoToDieStateIfPossible(); //0�ȉ��Ȃ�Ώ�Ԃ�Die�Ɉڍs
            GameObject smashCollider = Instantiate(smash.SmashCollider, transform); //SmashCollider�𐶐�
            smashCollider.transform.parent = transform; //�q�I�u�W�F�N�g��
            smashCollider.GetComponent<Smash>().StartTimer(smash.DestroyTime);
        }
    }

    /// <summary>
    /// ������vector��Weight�̃p�����[�^�[���|�����Ԃ�m�b�N�o�b�N������
    /// </summary>
    /// <param name="vector">�U�����q�b�g�����ۃm�b�N�o�b�N��������Ƌ���</param>
    public void Knockback(Vector3 vector)
    {
        transform.Translate(vector * status.Param.Weight, Space.World); //��ѓ���̕����Ƀm�b�N�o�b�N
    }

    /// <summary>
    /// �m�b�N�o�b�N������Obstacle�I�u�W�F�N�g�����݂��邩Ray�𓊎˂��Ĕ��肵�A
    /// ���݂����ꍇ�A�Փ˃_���[�W��^����ړI�̃��\�b�h
    /// </summary>
    /// <param name="vector">�U�����q�b�g�����ۃm�b�N�o�b�N��������Ƌ���</param>
    /// <param name="attack">�q�b�g�����U���̃_���[�W</param>
    private void JudgeObstacle(Vector3 vector, float attack)
    {
        Vector3 avoidSpace = transform.forward * -0.1f * status.Agent.radius; //������Ray��������Ȃ��悤�Ɍ��Ԃ����
        //�L�����̌���ɁAX���ɐ�����Ray�𔼌a�ܕ��������Ԋu�œ���
        Ray[] rays = new Ray[5]
        {
            new Ray(this.transform.position + transform.right * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * -0.5f * status.Agent.radius + transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.forward * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * 0.5f * status.Agent.radius + transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * status.Agent.radius + avoidSpace, vector.normalized),
        };

        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector * status.Param.Weight, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, (vector * status.Param.Weight).magnitude - avoidSpace.magnitude, 1 << 8))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    status.Damage(attack * 2f); //Hit�����U���̓�{�̃_���[�W��ǉ��ŗ^����
                    Instantiate(status.Effecter.GetEffectFromKey("ObstacleHit"), transform); //�G�t�F�N�g������������
                }
            }
        }
    }
}
