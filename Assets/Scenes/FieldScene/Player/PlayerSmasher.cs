using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmasher : MonoBehaviour
{
    private Smasher smasher;
    private float time;

    //SmashColliderの範囲内に入ったらコリダーを格納
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

    //敵の捕捉
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Add(smash); //敵のColliderを取得
        other.transform.localScale *= 2;
    }

    //敵の捕捉解除
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Remove(smash); //Colliderの範囲から外れた場合、リストから除外
        other.transform.localScale *= 0.5f;
    }

    //破棄された敵をリストから除外
    private void RemoveDestroyedEnemyInLockOn()
    {
        smashers.RemoveAll(x => x == null);
    }
}
