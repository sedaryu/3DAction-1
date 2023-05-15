using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmasher : MonoBehaviour
{
    private Smasher smasher;
    private float time;

    //SmashCollider�͈͓̔��ɓ�������R���_�[���i�[
    public List<Smasher> smashers = new List<Smasher>();

    public void SetSmasher(Smasher _smasher, float _time)
    {
        smasher = _smasher;
        time = _time;
    }

    public void MakeGroggy(List<Collider> others)
    {
        others.ForEach(x => x.GetComponent<IGrogable>().Grog(smasher, time));
    }

    public void Smash(float smashTime, float knockback, float attack)
    {
        RemoveDestroyedEnemyInLockOn();
        smashers.ForEach(x => x.StopTimer());
        StartCoroutine(SmashTime(smashTime, knockback, attack));
    }

    private IEnumerator SmashTime(float time, float knockback, float attack)
    { 
        yield return new WaitForSeconds(time);
        foreach (Smasher x in smashers) 
        {
            List<Collider> hits = x.Smash(knockback, attack);
            if (hits != null) MakeGroggy(hits);
        }

    }

    //�G�̕ߑ�
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Add(smash); //�G��Collider���擾
        other.transform.localScale *= 2;
    }

    //�G�̕ߑ�����
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Remove(smash); //Collider�͈̔͂���O�ꂽ�ꍇ�A���X�g���珜�O
        other.transform.localScale *= 0.5f;
    }

    //�j�����ꂽ�G�����X�g���珜�O
    private void RemoveDestroyedEnemyInLockOn()
    {
        smashers.RemoveAll(x => x == null);
    }
}
