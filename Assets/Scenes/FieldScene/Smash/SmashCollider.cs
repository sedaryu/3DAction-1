using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SmashCollider : MonoBehaviour
{
    private List<EnemyAct> enemies = new List<EnemyAct>();

    private MeshRenderer meshRenderer;

    private IEnumerator coroutine;

    public void StartTimer(float time)
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
            if (delta >= time * 0.01f)
            {
                meshRenderer.material.color -= new Color32(0, 0, 0, 1);
                delta = 0;
            }
            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }

    public async Task PlayerSmashEnemies(SmashParam smashParam)
    {
        StopCoroutine(coroutine);
        await Task.Delay((int)(smashParam.SmashTime * 1000));
        RemoveDestroyedEnemy();
        enemies.ForEach(x => x.Hit((x.transform.position - transform.position).normalized *
                             smashParam.Knockback, smashParam.Attack, smashParam));
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject.GetComponent<EnemyAct>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject.GetComponent<EnemyAct>());
        }
    }

    //”jŠü‚³‚ê‚½EnemyDamage‚ðƒŠƒXƒg‚©‚çœŠO
    private void RemoveDestroyedEnemy()
    {
        enemies.RemoveAll(x => x == null);
    }
}
