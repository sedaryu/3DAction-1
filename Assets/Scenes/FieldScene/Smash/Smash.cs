using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    private List<EnemyDamage> enemies = new List<EnemyDamage>();

    private Material material;

    private IEnumerator coroutine;

    void Start()
    { 
        material = GetComponent<Material>();
    }

    public void StartTimer(float time)
    {
        coroutine = TimerToDestroy(time);
        StartCoroutine(coroutine);
    }

    public IEnumerator TimerToDestroy(float time)
    {
        float sum = 0;
        float delta = 0;
        while (sum <= time)
        { 
            sum += Time.deltaTime;
            delta += Time.deltaTime;
            if (delta >= 1)
            {
                material.color -= new Color32(0, 0, 0, 1);
                delta = 0;
            }
            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }

    public IEnumerator PlayerCombatAttackEnemies(SmashParam smashParam, GameObject effect)
    {
        StopCoroutine(coroutine);
        yield return new WaitForSeconds(smashParam.SmashTime);
        RemoveDestroyedEnemy();
        Instantiate(effect, transform.position, Quaternion.identity);
        enemies.ForEach(x => x.Hit((x.transform.position - transform.position).normalized *
                             smashParam.Knockback, smashParam.Attack, smashParam));
        Destroy(transform.parent.gameObject);
    }

    public void EnemyInCombatRange(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
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
