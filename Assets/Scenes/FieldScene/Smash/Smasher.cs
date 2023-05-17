using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Smasher : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private IEnumerator timer;

    private List<Collider> targetingEnemies = new List<Collider>();

    public void StartTimer(float time)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        timer = WaitTimeToDestroy(time);
        StartCoroutine(timer);
    }

    public IEnumerator WaitTimeToDestroy(float time)
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

    public void StopTimer()
    {
        StopCoroutine(timer);
    }

    public List<Collider> Smash(float knockback, float attack)
    {
        RemoveDestroyedEnemy();
        //Instantiate(effect, transform.position, Quaternion.identity);
        targetingEnemies.ForEach(x => x.GetComponent<ITargetable>().Hit((x.transform.position - transform.position).normalized * knockback, attack));
        Destroy(transform.parent.gameObject, 0.02f);
        return targetingEnemies.Where(x => x.TryGetComponent<IGrogable>(out IGrogable grog) == true && grog.Groggy == true).ToList();
    }

    //“G‚Ì•ß‘¨
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(other); //“G‚ÌCollider‚ğæ“¾
    }

    //“G‚Ì•ß‘¨‰ğœ
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(other); //Collider‚Ì”ÍˆÍ‚©‚çŠO‚ê‚½ê‡AƒŠƒXƒg‚©‚çœŠO
    }

    //”jŠü‚³‚ê‚½“G‚ğƒŠƒXƒg‚©‚çœŠO
    private void RemoveDestroyedEnemy()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
