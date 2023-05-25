using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSmasher : MonoBehaviour
{
    private Animator animator;

    private Smash smash;

    public bool IsSmashing { get; private set; } = false;

    public UnityAction onKilling;

    //SmashColliderの範囲内に入ったらコリダーを格納
    public List<Smash> smashers = new List<Smash>();

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSmash(Smash _smash)
    {
        smash = _smash;
    }

    public void MakeGroggy(List<Collider> others)
    {
        foreach (Collider x in others) 
        {
            onKilling.Invoke();
            if (x.TryGetComponent<IGrogable>(out IGrogable grog)) grog.Grog(smash);
        }
    }

    public void Smash()
    {
        if (IsSmashing) return;
        IsSmashing = true;
        smashers.ForEach(x => x.StopTimer());
        StartCoroutine(SmashTime());
    }

    private IEnumerator SmashTime()
    {
        animator.SetTrigger("StartSmash");
        yield return new WaitForSeconds(smash.Param.SmashTime * 0.8f);
        animator.SetTrigger("FinishSmash");
        yield return new WaitForSeconds(smash.Param.SmashTime * 0.2f);
        animator.SetTrigger("ExitSmash");
        IsSmashing = false;
        RemoveColliderInSmashers();
        foreach (Smash x in smashers) 
        {
            Instantiate(smash.Param.SmashEffect, x.transform.position, x.transform.rotation);
            List<Collider> hits = x.Smashing();
            if (hits != null) MakeGroggy(hits);
        }
    }

    //コリダーの捕捉
    public void PlayerEnetrCollider(Collider other)
    {
        if (!other.TryGetComponent<Smash>(out Smash smash)) return;
        smashers.Add(smash); //敵のColliderを取得
        other.transform.localScale *= 2;
    }

    //コリダーの捕捉解除
    public void PlayerExitCollider(Collider other)
    {
        if (!other.TryGetComponent<Smash>(out Smash smash)) return;
        smashers.Remove(smash); //Colliderの範囲から外れた場合、リストから除外
        other.transform.localScale *= 0.5f;
    }

    //破棄されたコリダーをリストから除外
    public bool RemoveColliderInSmashers()
    {
        smashers.RemoveAll(x => x == null);
        if(smashers.Count > 0) return false;
        return true;
    }
}
