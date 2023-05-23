using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; 

    private float spawnArea_x;
    private float spawnArea_z;

    private Transform player;

    private float time;

    private void Awake()
    {
        spawnArea_x = transform.localScale.x * 5;
        spawnArea_z = transform.localScale.z * 5;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2.5f)
        { 
            SpawnEnemy();
            time = 0;
        }
    }

    private void SpawnEnemy()
    {
        GameObject spawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Vector3 spawnPosition;

        if (player.position.x < 0 && 0 < player.position.z)
        { spawnPosition = new Vector3(Random.Range(0, spawnArea_x), 0, Random.Range(-spawnArea_z, 0)); }
        else if (player.position.x < 0 && player.position.z < 0)
        { spawnPosition = new Vector3(Random.Range(0, spawnArea_x), 0, Random.Range(0, spawnArea_z)); }
        else if (0 < player.position.x && 0 < player.position.z)
        { spawnPosition = new Vector3(Random.Range(-spawnArea_x, 0), 0, Random.Range(-spawnArea_z, 0)); }
        else if (0 < player.position.x && player.position.z < 0)
        { spawnPosition = new Vector3(Random.Range(-spawnArea_x, 0), 0, Random.Range(0, spawnArea_z)); }
        else
        { return; }

        Instantiate(spawn, spawnPosition, Quaternion.identity);
    }
}
