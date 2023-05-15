using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effecter;

public class EnemyKnockbacker : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Knockback(Vector3 vector)
    {
        transform.Translate(vector, Space.World); //��ѓ���̕����Ƀm�b�N�o�b�N
        animator.SetTrigger("Damage"); //��_���[�W�̍ۂ̃A�j���[�V���������s
    }

    /// <summary>
    /// �m�b�N�o�b�N������Obstacle�I�u�W�F�N�g�����݂��邩Ray�𓊎˂��Ĕ��肵�A
    /// ���݂����ꍇ�A�Փ˃_���[�W��^����ړI�̃��\�b�h
    /// </summary>
    /// <param name="vector">�U�����q�b�g�����ۃm�b�N�o�b�N��������Ƌ���</param>
    public int JudgeObstacle(Transform transform, float radius, Vector3 vector)
    {
        transform.rotation = Quaternion.LookRotation(-vector.normalized); //�v���C���[�̕���������

        Vector3 avoidSpace = transform.forward * -0.1f * radius; //������Ray��������Ȃ��悤�Ɍ��Ԃ����
        //�L�����̌���ɁAX���ɐ�����Ray�𔼌a�ܕ��������Ԋu�œ���
        Ray[] rays = new Ray[5]
        {
            new Ray(transform.position + transform.right * -radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.right * -0.5f * radius + transform.forward * -0.86f * radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.forward * -radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.right * 0.5f * radius + transform.forward * -0.86f * radius + avoidSpace, vector.normalized),
            new Ray(transform.position + transform.right * radius + avoidSpace, vector.normalized),
        };

        int critical = 0;
        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, vector.magnitude - avoidSpace.magnitude, 1 << 8))
            {
                critical++;
            }
        }
        return critical;
    }
}
