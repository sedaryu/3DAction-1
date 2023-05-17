using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmasher : MonoBehaviour
{
    private Animator animator;

    private Smasher smasher;
    private float time;

    //SmashColliderの範囲内に入ったらコリダーを格納
    public List<Smasher> smashers = new List<Smasher>();

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSmasher(Smasher _smasher, float _time)
    {
        smasher = _smasher;
        time = _time;
    }

    public void MakeGroggy(List<Collider> others)
    {
        foreach (Collider x in others) 
        {
            x.TryGetComponent<IGrogable>(out IGrogable grog);
            grog.Grog(smasher, time);
        }
    }

    public void Smash(float smashTime, float knockback, float attack, GameObject smashEffect)
    {
        RemoveColliderInSmashers();
        smashers.ForEach(x => x.StopTimer());
        StartCoroutine(SmashTime(smashTime, knockback, attack, smashEffect));
    }

    private IEnumerator SmashTime(float time, float knockback, float attack, GameObject smashEffect)
    {
        animator.SetTrigger("StartSmash");
        yield return new WaitForSeconds(time * 0.8f);
        animator.SetTrigger("FinishSmash");
        yield return new WaitForSeconds(time * 0.2f);
        foreach (Smasher x in smashers) 
        {
            Instantiate(smashEffect, x.transform.position, x.transform.rotation);
            List<Collider> hits = x.Smash(knockback, attack);
            if (hits != null) MakeGroggy(hits);
        }
    }

    //コリダーの捕捉
    public void PlayerEnetrCollider(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Add(smash); //敵のColliderを取得
        other.transform.localScale *= 2;
    }

    //コリダーの捕捉解除
    public void PlayerExitCollider(Collider other)
    {
        if (!other.TryGetComponent<Smasher>(out Smasher smash)) return;
        smashers.Remove(smash); //Colliderの範囲から外れた場合、リストから除外
        other.transform.localScale *= 0.5f;
    }

    //破棄されたコリダーをリストから除外
    private void RemoveColliderInSmashers()
    {
        smashers.RemoveAll(x => x == null);
    }
}
