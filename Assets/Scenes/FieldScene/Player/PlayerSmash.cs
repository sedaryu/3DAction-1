using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmash : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //コントローラー
    private PlayerController controller;

    //取得したコリダー
    private List<GameObject> enemies = new List<GameObject>();

    private bool smash;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        Smashing(controller.InputCombating());
    }

    public void PlayerInCombatRange(Collider other)
    {
        if (other.CompareTag("Combat"))
        {
            other.transform.localScale = new Vector3(4, 4, 4);
            enemies.Add(other.transform.parent.gameObject);
        }
    }

    public void PlayerOutCombatRange(Collider other)
    {
        if (other.CompareTag("Combat"))
        {
            other.transform.localScale = new Vector3(2, 2, 2);
            enemies.Remove(other.transform.parent.gameObject);
        }
    }

    private void Smashing(bool input)
    {
        if (!input) return;
        if (status.IsNoMoveInvincible) return;
        if (enemies.Count <= 0) return;
        if (RemoveDestroyedCollider()) return;
        enemies.ForEach(x => StartCoroutine(x.GetComponentInChildren<Smash>()
               .PlayerCombatAttackEnemies(status.SmashParam, status.Effecter.GetEffectFromKey("Smash"))));
        StartCoroutine(SmashTime());
    }

    private IEnumerator SmashTime()
    {
        status.Animator.SetTrigger("StartSmash");
        status.GoToNoMoveInvincibleStateIfPossible();
        yield return new WaitForSeconds(status.SmashParam.SmashTime * 0.8f);
        status.Animator.SetTrigger("FinishSmash");
        yield return new WaitForSeconds(status.SmashParam.SmashTime * 0.2f);
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
