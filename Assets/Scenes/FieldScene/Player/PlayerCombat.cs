using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //コントローラー
    private PlayerController controller;

    private List<GameObject> enemies = new List<GameObject>();

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        Combating(controller.InputCombating());
    }

    public void EnemyInCombatRange(Collider other)
    {
        if (other.CompareTag("Combat"))
        {
            other.transform.localScale = new Vector3(4, 4, 4);
            enemies.Add(other.transform.parent.gameObject);
        }
    }

    public void EnemyOutCombatRange(Collider other)
    {
        if (other.CompareTag("Combat"))
        {
            other.transform.localScale = new Vector3(2, 2, 2);
            enemies.Remove(other.transform.parent.gameObject);
        }
    }

    private void Combating(bool input)
    {
        if (!input) return;
        if (enemies.Count <= 0) return;
        RemoveDestroyedEnemy();
        enemies.ForEach(x => x.GetComponent<EnemyGroggy>().StopCoroutine("GroggyTime"));
        enemies.ForEach(x => StartCoroutine(CombatTime(x)));
    }

    private IEnumerator CombatTime(GameObject other)
    {
        status.GoToNoMoveInvincibleStateIfPossible();
        yield return new WaitForSeconds(1.25f);
        Destroy(other);
        status.GoToNormalStateIfPossible();
    }

    //破棄された敵をリストから除外
    private void RemoveDestroyedEnemy()
    {
        enemies.RemoveAll(x => x == null);
    }
}
