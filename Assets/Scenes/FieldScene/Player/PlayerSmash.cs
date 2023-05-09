using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerSmash
{
    //ステータス
    private PlayerStatus status;
    //エフェクター
    private MobEffecter effecter;

    //取得したコリダーオブジェクト
    private List<GameObject> colliders = new List<GameObject>();

    public PlayerSmash(PlayerStatus _status, MobEffecter _effecter)
    {
        status = _status;
        effecter = _effecter;
    }

    public void PlayerInSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = new Vector3(4, 4, 4);
            colliders.Add(other.transform.parent.gameObject);
        }
    }

    public void PlayerOutSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = new Vector3(2, 2, 2);
            colliders.Remove(other.transform.parent.gameObject);
        }
    }

    public void Smash(bool input)
    {
        if (!input) return;
        if (status.IsNoMoveInvincible) return;
        if (colliders.Count <= 0) return;
        if (RemoveDestroyedCollider()) return;
        foreach (GameObject x in colliders) 
        {
            Task _0 = x.GetComponentInChildren<SmashCollider>().PlayerSmashEnemies(status.SmashParam);
            Task _1 = SmashTime(x.transform.position, x.transform.rotation);
        }
    }

    private async Task SmashTime(Vector3 position, Quaternion rotation)
    {
        status.Animator.SetTrigger("StartSmash");
        status.GoToNoMoveInvincibleStateIfPossible();
        await Task.Delay((int)(status.SmashParam.SmashTime * 700));
        status.Animator.SetTrigger("FinishSmash");
        await Task.Delay((int)(status.SmashParam.SmashTime * 300));
        effecter.InstanceEffect(status.SmashParam.SmashEffect, position, rotation);
        status.GoToNormalStateIfPossible();
    }

    //破棄されたコリダーをリストから除外
    private bool RemoveDestroyedCollider()
    {
        colliders.RemoveAll(x => x == null);
        if (colliders.Count == 0) return true;
        return false;
    }
}
