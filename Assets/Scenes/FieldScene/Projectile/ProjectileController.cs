using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 25 * Time.deltaTime));
    }

    private IEnumerator DestroyProjectile() //一定時間経過後飛び道具が消滅する処理
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
