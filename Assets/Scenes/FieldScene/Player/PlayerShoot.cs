using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private ProjectileParam param;

    //捕捉した敵オブジェクトを格納
    List<EnemyController> lockOnEnemies = new List<EnemyController>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && lockOnEnemies.Count > 0)
        {
            List<float> distances = lockOnEnemies.Select(x => Vector3.Distance(transform.position, x.transform.position)).ToList();
            EnemyController enemy = lockOnEnemies[distances.FindIndex(x => x == distances.Min())];
            transform.LookAt(enemy.transform);
            Vector3 vector = transform.forward.normalized * param.Knockback;
            enemy.Hit(vector, param.Attack);
            //Instantiate(projectilePrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        }
    }

    //敵オブジェクトの捕捉
    public void EnemyInCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Add(other.GetComponent<EnemyController>());
        }
    }

    //敵オブジェクトの捕捉解除
    public void EnemyOutCollider(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            lockOnEnemies.Remove(other.GetComponent<EnemyController>());
        }
    }
}
