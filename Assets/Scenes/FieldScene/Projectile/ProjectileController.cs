using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public ProjectileParam Param
    {
        get => _param;
    }
    [SerializeField] private ProjectileParam _param;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, Param.Speed * Time.deltaTime));
    }

    private IEnumerator DestroyProjectile() //ˆê’èŠÔŒo‰ßŒã”ò‚Ñ“¹‹ï‚ªÁ–Å‚·‚éˆ—
    {
        yield return new WaitForSeconds(Param.Reach);
        Destroy(gameObject);
    }
}
