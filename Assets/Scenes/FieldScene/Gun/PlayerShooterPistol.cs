using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooterPistol : PlayerShooter
{
    protected override List<Collider> HittingEnemy(List<Collider> targets)
    {
        List<float> distances = targets.Select(x => Vector3.Distance(transform.position, x.transform.position)).ToList(); //�G�ƃv���C���[�̋��������X�g��
        int i = distances.FindIndex(x => x == distances.Min()); //���̒��ōł��Z�������̃C���f�b�N�X�ԍ����擾
        if (targets.Count - 1 < i || i == -1) return null;
        List<Collider> targetList = new List<Collider>();
        targetList.Add(targets[i]);  //�G��Collider���擾
        return targetList;
    }

    protected override void LookAt(Transform trans)
    {
        transform.LookAt(trans);
    }
}
