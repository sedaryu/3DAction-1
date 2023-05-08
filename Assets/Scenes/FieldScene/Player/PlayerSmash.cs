using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerSmash
{
    //ステータス
    private PlayerStatus status;

    //取得したコリダー
    private List<GameObject> enemies = new List<GameObject>();

    public PlayerSmash(PlayerStatus _status)
    {
        status = _status;
    }

    public void PlayerInSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = new Vector3(4, 4, 4);
            enemies.Add(other.transform.parent.gameObject);
        }
    }

    public void PlayerOutSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = new Vector3(2, 2, 2);
            enemies.Remove(other.transform.parent.gameObject);
        }
    }

    public void Smash(bool input)
    {
        if (!input) return;
        if (status.IsNoMoveInvincible) return;
        if (enemies.Count <= 0) return;
        if (RemoveDestroyedCollider()) return;
        enemies.ForEach(x => x.GetComponentInChildren<Smash>().PlayerCombatAttackEnemies(status.SmashParam, status.Effecter.GetEffectFromKey("Smash")));
        SmashTime();
    }

    private async Task SmashTime()
    {
        status.Animator.SetTrigger("StartSmash");
        status.GoToNoMoveInvincibleStateIfPossible();
        await Task.Delay((int)(status.SmashParam.SmashTime * 800));
        status.Animator.SetTrigger("FinishSmash");
        await Task.Delay((int)(status.SmashParam.SmashTime * 200));
        status.GoToNormalStateIfPossible();
    }

    //破棄されたコリダーをリストから除外
    private bool RemoveDestroyedCollider()
    {
        enemies.RemoveAll(x => x == null);
        if (enemies.Count == 0) return true;
        return false;
    }
}
