using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private List<string> collectItems = new List<string>();
    private int countOfCollecting = 0;

    private Vector3 spawnArea;
    private float spawnArea_x;
    private float spawnArea_z;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GameObject.Find("BakedPlane").transform.position;
        spawnArea_x = spawnArea.x * 5;
        spawnArea_z = spawnArea.z * 5;
        player = GameObject.Find("BakedPlane").transform;
        MovePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectItems(List<string> items)
    { 
        collectItems.AddRange(items);
        countOfCollecting++;
        StartCoroutine(MovePosition());

        if (countOfCollecting == 5) SummaryGame();
    }

    private void SummaryGame()
    {
        List<string> itemNames = collectItems.Distinct().ToList();
        List<int> itemNumbers = new List<int>();

        for (int i = 0; i < itemNames.Count; i++)
        {
            itemNumbers.Add(collectItems.Count(x => x == itemNames[i]));
        }

        for (int i = 0; i < itemNames.Count; i++)
        {
            Debug.Log($"{itemNames[i]} : {itemNumbers[i]}");
        }
    }

    private IEnumerator MovePosition()
    {
        yield return null;
        Vector3 position;
        while (true)
        {
            position = new Vector3(Random.Range(-spawnArea_x, spawnArea_x), 0, Random.Range(-spawnArea_z, spawnArea_z));
            if (Vector3.Distance(position, player.position) > 5) break;
        }
        transform.position = position;
    }
}
