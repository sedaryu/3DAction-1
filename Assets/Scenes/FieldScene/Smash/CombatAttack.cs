using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAttack : MonoBehaviour
{

    private List<EnemyDamage> enemies = new List<EnemyDamage>();

    [SerializeField] private GameObject combatEffect;

    private bool combat;

    public IEnumerator PlayerCombatAttackEnemies(SmashParam smashParam)
    {
        if (!combat)
        {
            combat = true;
            yield return new WaitForSeconds(smashParam.WaitTime);
            RemoveDestroyedEnemy();
            Instantiate(combatEffect, transform.position, Quaternion.identity);
            enemies.ForEach(x => x.Hit((x.transform.position - transform.position).normalized * 
                                 smashParam.Knockback, smashParam.Attack));
            Destroy(transform.parent.gameObject);
        }
    }

    public void EnemyInCombatRange(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("InCollider");
            enemies.Add(other.gameObject.GetComponent<EnemyDamage>());
        }
    }

    public void EnemyOutCombatRange(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject.GetComponent<EnemyDamage>());
        }
    }

    //”jŠü‚³‚ê‚½EnemyDamage‚ðƒŠƒXƒg‚©‚çœŠO
    private void RemoveDestroyedEnemy()
    {
        enemies.RemoveAll(x => x == null);
    }
}
