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

    //private ScoreController scoreController;

    public void StartTimer(float time)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        timer = WaitTimeToDestroy(time);
        StartCoroutine(timer);

        //scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        //scoreController.IncreaseRushTime();
        //scoreController.IncreaseCombo();
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

        //scoreController.IncreaseScore();

        return targetingEnemies.Where(x => x.GetComponent<ITargetable>().IsGroggy == true).ToList();
    }

    //�G�̕ߑ�
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(other); //�G��Collider���擾
    }

    //�G�̕ߑ�����
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(other); //Collider�͈̔͂���O�ꂽ�ꍇ�A���X�g���珜�O
    }

    //�j�����ꂽ�G�����X�g���珜�O
    private void RemoveDestroyedEnemy()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
