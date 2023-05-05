using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroggy : MonoBehaviour
{
    //ステータス
    private EnemyStatus status;

    private bool groggy;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (status.IsSmashable && !groggy)
        //{
        //    groggy = true;
        //    transform.Find("SmashCollider").gameObject.SetActive(true);
        //    StartCoroutine("GroggyTime");
        //}
    }

    private IEnumerator GroggyTime()
    {
        yield return new WaitForSeconds(6.5f);
        Destroy(gameObject);
    }
}
