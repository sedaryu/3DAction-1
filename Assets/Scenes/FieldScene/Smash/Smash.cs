using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Smash : MonoBehaviour
{
    private List<EnemyDamage> enemies = new List<EnemyDamage>();

    private MeshRenderer meshRenderer;

    private CancellationTokenSource cancel;

    public void StartTimer(float time)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        cancel = new CancellationTokenSource();
        TimerToDestroy(time, cancel.Token);
    }

    public async Task TimerToDestroy(float time, CancellationToken cancel)
    {
        float sum = 0;
        float delta = 0;
        while (sum <= time)
        { 
            sum += Time.deltaTime;
            delta += Time.deltaTime;
            if (delta >= time * 0.01f)
            {
                meshRenderer.material.color -= new Color32(0, 0, 0, 1);
                delta = 0;
            }
            await Task.Delay(20, cancel);
        }
        Destroy(transform.parent.gameObject);
    }

    public async Task PlayerCombatAttackEnemies(SmashParam smashParam, GameObject effect)
    {
        cancel.Cancel();
        await Task.Delay((int)(smashParam.SmashTime * 1000));
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
