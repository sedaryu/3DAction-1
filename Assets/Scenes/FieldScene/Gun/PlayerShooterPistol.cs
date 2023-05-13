using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooterPistol : PlayerShooter
{
    public override Transform HittingEnemy(Vector3 position, List<ITargetable> targets, float knockback, float attack)
    {
        List<float> distances = targets.Select(x => Vector3.Distance(position, x.Transform.position)).ToList(); //�G�ƃv���C���[�̋��������X�g��
        int i = distances.FindIndex(x => x == distances.Min()); //���̒��ōł��Z�������̃C���f�b�N�X�ԍ����擾
        if (targets.Count - 1 < i || i == -1) return null;
        ITargetable target = targets[i];  //�G�̃X�N���v�g���擾
        Vector3 vector = (position - target.Transform.position).normalized * knockback; //�m�b�N�o�b�N������ݒ�
        target.Hit(vector, attack); //ITargetable��Hit���\�b�h�ɒl��n�����s
        return target.Transform;
    }
}
