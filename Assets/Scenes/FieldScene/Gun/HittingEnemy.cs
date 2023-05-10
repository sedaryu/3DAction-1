using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    public void PistolHittingEnemy(Transform player, List<EnemyAct> enemies, GunParam gun, SmashParam smash)
    {
        List<float> distances = enemies.Select(x => Vector3.Distance(player.position, x.transform.position)).ToList(); //�G�ƃv���C���[�̋��������X�g��
        int i = distances.FindIndex(x => x == distances.Min()); //���̒��ōł��Z�������̃C���f�b�N�X�ԍ����擾
        if (enemies.Count - 1 < i || i == -1) return;
        EnemyAct enemy = enemies[i];  //�G�̃X�N���v�g���擾
        player.LookAt(enemy.transform); //�G�̕������v���C���[������
        Vector3 vector = player.forward.normalized * gun.Knockback; //�m�b�N�o�b�N������ݒ�
        enemy.Hit(vector, gun.Attack, smash); //�G�X�N���v�g�̃_���[�W���\�b�h�ɒl��n�����s
    }
}
