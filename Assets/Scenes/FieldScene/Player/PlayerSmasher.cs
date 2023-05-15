using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmasher : MonoBehaviour
{
    private Smasher smasher;
    private float time;

    //SmashCollider‚Ì”ÍˆÍ“à‚É“ü‚Á‚½‚çƒRƒŠƒ_[‚ğŠi”[
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

    //“G‚Ì•ß‘¨
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Add(smash); //“G‚ÌCollider‚ğæ“¾
        other.transform.localScale *= 2;
    }

    //“G‚Ì•ß‘¨‰ğœ
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Remove(smash); //Collider‚Ì”ÍˆÍ‚©‚çŠO‚ê‚½ê‡AƒŠƒXƒg‚©‚çœŠO
        other.transform.localScale *= 0.5f;
    }

    //”jŠü‚³‚ê‚½“G‚ğƒŠƒXƒg‚©‚çœŠO
    private void RemoveDestroyedEnemyInLockOn()
    {
        smashers.RemoveAll(x => x == null);
    }
}
