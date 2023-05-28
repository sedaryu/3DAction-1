using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Smash : MonoBehaviour
{
    public SmashParam Param { get => param; }

    [SerializeField] private SmashParam param;

    private MeshRenderer meshRenderer;

    private IEnumerator timer;

    private List<Collider> targetingEnemies = new List<Collider>();

    //private ScoreController scoreController;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartTimer(param.DestroyTime);
    }

    public void StartTimer(float time)
    {
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

    public List<Collider> Smashing()
    {
        RemoveDestroyedEnemy();
        targetingEnemies.Remove(transform.parent.gameObject.GetComponent<Collider>());
        transform.parent.gameObject.GetComponent<ITargetable>().Die();
        if (targetingEnemies.Count <= 0) return null;
        targetingEnemies.ForEach(x => x.GetComponent<ITargetable>().Hit((x.transform.position - transform.position).normalized * param.Knockback, param.Attack));
        Destroy(gameObject);
        return targetingEnemies.Where(x => x.GetComponent<ITargetable>().IsGroggy == true).ToList();
    }

    //ìGÇÃïﬂë®
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(other); //ìGÇÃColliderÇéÊìæ
    }

    //ìGÇÃïﬂë®âèú
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(other); //ColliderÇÃîÕàÕÇ©ÇÁäOÇÍÇΩèÍçáÅAÉäÉXÉgÇ©ÇÁèúäO
    }

    //îjä¸Ç≥ÇÍÇΩìGÇÉäÉXÉgÇ©ÇÁèúäO
    private void RemoveDestroyedEnemy()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
