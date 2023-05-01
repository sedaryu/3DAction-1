using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //コントローラー
    private PlayerController controller;

    //取得したコリダー
    private List<GameObject> colliders = new List<GameObject>();

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        Combating(controller.InputCombating());
    }

    public void PlayerInCombatRange(Collider other)
    {
        if (other.CompareTag("Combat"))
        {
            other.transform.localScale = new Vector3(4, 4, 4);
            colliders.Add(other.transform.parent.gameObject);
        }
    }

    public void PlayerOutCombatRange(Collider other)
    {
        if (other.CompareTag("Combat"))
        {
            other.transform.localScale = new Vector3(2, 2, 2);
            colliders.Remove(other.transform.parent.gameObject);
        }
    }

    private void Combating(bool input)
    {
        if (!input) return;
        if (colliders.Count <= 0) return;
        RemoveDestroyedEnemy();
        colliders.ForEach(x => x.GetComponent<EnemyGroggy>().StopCoroutine("GroggyTime"));
        colliders.ForEach(x => StartCoroutine(x.GetComponentInChildren<CombatAttack>().PlayerCombatAttackEnemies()));
        CombatTime();
    }

    private IEnumerator CombatTime()
    {
        status.GoToNoMoveInvincibleStateIfPossible();
        yield return new WaitForSeconds(1.25f);
        status.GoToNormalStateIfPossible();
    }

    //破棄されたコリダーをリストから除外
    private void RemoveDestroyedEnemy()
    {
        colliders.RemoveAll(x => x == null);
    }
}
