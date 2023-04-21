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

    private IEnumerator DestroyProjectile() //��莞�Ԍo�ߌ��ѓ�����ł��鏈��
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
