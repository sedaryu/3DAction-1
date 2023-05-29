using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnObjectList spawnEnemyList;

    private Vector3 spawnArea;
    private float spawnArea_x;
    private float spawnArea_z;

    private GameObject[] spawnObjects;

    private Transform player;

    private float time;
    private List<int> random = new List<int>();

    private List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        spawnArea = GameObject.Find("BakedPlane").transform.localScale;
        spawnArea_x = spawnArea.x * 5;
        spawnArea_z = spawnArea.z * 5;

        for (int i = 0; i < spawnEnemyList.SpawnObjects.Length; i++)
        {
            for (int n = 0; n < spawnEnemyList.SpawnObjects[i].appearanceProbability; n++)
            {
                random.Add(i);
            }
        }

        GetSpawnObject();
    }

    private void GetSpawnObject()
    {
        string[] enemiesName = spawnEnemyList.SpawnObjects.ToList().Select(x => x.name).ToArray();
        spawnObjects = GameObject.Find("LoadAsset").GetComponent<LoadAsset>().LoadObjects("Enemy", enemiesName);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        for (int i = 0; i < 20; i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnEnemyList.SpawnTime)
        {
            if (JudgeSpawnEnemy() && player != null) SpawnEnemy();
            time = 0;
        }
    }

    private void SpawnEnemy()
    {
        GameObject spawn = spawnObjects[random[Random.Range(0, 100)]];
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

        GameObject enemy = Instantiate(spawn, spawnPosition, Quaternion.identity);
        enemies.Add(enemy);
    }

    private bool JudgeSpawnEnemy()
    {
        enemies.RemoveAll(x => x == null);
        if (enemies.Count > 30) return false;
        return true;
    }
}
