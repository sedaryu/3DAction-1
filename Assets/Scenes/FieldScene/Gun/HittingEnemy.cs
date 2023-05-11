using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    public Transform PistolHittingEnemy(Vector3 position, List<EnemyReferenced> enemies, float knockback, float attack)
    {
        List<float> distances = enemies.Select(x => Vector3.Distance(position, x.transform.position)).ToList(); //�G�ƃv���C���[�̋��������X�g��
        int i = distances.FindIndex(x => x == distances.Min()); //���̒��ōł��Z�������̃C���f�b�N�X�ԍ����擾
        if (enemies.Count - 1 < i || i == -1) return null;
        EnemyReferenced enemy = enemies[i];  //�G�̃X�N���v�g���擾
        //Vector3 vector = player.forward.normalized * knockback; //�m�b�N�o�b�N������ݒ�
        Vector3 vector = (position - enemy.transform.position).normalized * knockback; //�m�b�N�o�b�N������ݒ�
        enemy.OnAttacked(vector, attack); //�G�X�N���v�g�̃_���[�W���\�b�h�ɒl��n�����s
        return enemy.transform;
    }
}
