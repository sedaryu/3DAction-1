using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public SpawnObjectList spawnItemList;

    private GameObject[] spawnObjects;

    private List<int> random = new List<int>();

    private void Awake()
    {
        SettingRandom();

        GetSpawnObject();
    }

    private void SettingRandom()
    {
        int randamMax = 100;
        for (int i = 0; i < spawnItemList.SpawnObjects.Length; i++)
        {
            randamMax -= spawnItemList.SpawnObjects[i].appearanceProbability;

            for (int n = 0; n < spawnItemList.SpawnObjects[i].appearanceProbability; n++)
            {
                random.Add(i);
            }
        }

        for (int i = 0; i < randamMax; i++)
        {
            random.Add(spawnItemList.SpawnObjects.Length);
        }
    }

    private void GetSpawnObject()
    {
        string[] itemsPath = spawnItemList.SpawnObjects.ToList().Select(x => x.name).ToArray();
        spawnObjects = GameObject.Find("LoadAsset").GetComponent<LoadAsset>().LoadObjects("Item", itemsPath);
    }

    public void SpawnItem(Vector3 position)
    {
        int rnd = random[Random.Range(0, 100)];
        if (rnd == spawnItemList.SpawnObjects.Length) return;
        Vector3 navmeshPosition = NavMesh.SamplePosition(position, out NavMeshHit hit, 10, 1) ? hit.position : new Vector3(0, 0, 0);
        Instantiate(spawnObjects[rnd], navmeshPosition, Quaternion.identity);
        Debug.Log("Spawn!!!");
    }
}
