using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <Summary>
/// ���̃X�N���v�g���A�^�b�`�����I�u�W�F�N�g�ƁA
/// MainCamera�̊Ԃɑ��݂����Q���𓧉߂�����ړI�̃N���X
/// </Summary>
public class TransObstacle : MonoBehaviour
{
    //���C���΂���
    private Transform target;
    //�O�t���[�����C��Hit����Obstacle
    private MakeTransparent obstacle;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastToTarget();
    }

    /// <summary>
    /// ���C�𓊎˂��A�q�b�g����Obstacle���C���[����MakeTransparent�X�N���v�g���A�^�b�`���ꂽ�I�u�W�F�N�g��
    /// ���߂�����ړI�̃��\�b�h
    /// </summary>
    private void RaycastToTarget()
    {
        if (target == null) return;
        Vector3 directionToTarget = target.position - transform.position; //�^�[�Q�b�g�����ւ̃x�N�g�����擾
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, directionToTarget.magnitude, 1 << 8))
        {
            obstacle = hit.transform.gameObject.GetComponent<MakeTransparent>();
            if (obstacle == null) return;
            obstacle.Transparent();
        }
        else
        {
            //��Q���̓��߂���������ꍇ
            if (obstacle != null)
            {
                obstacle.CancelTransparent();
                obstacle = null;
            }
        }
    }
}
