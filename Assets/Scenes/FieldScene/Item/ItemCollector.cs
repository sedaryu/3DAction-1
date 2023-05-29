using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int collectCount;

    private List<string> collectItems = new List<string>();
    private int countOfCollecting = 0;
    private float time = 180;

    private Vector3 spawnArea;
    private float spawnArea_x;
    private float spawnArea_z;

    private Transform player;

    List<IPlayerUI> playerUIs;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GameObject.Find("BakedPlane").transform.localScale;
        spawnArea_x = spawnArea.x * 5;
        spawnArea_z = spawnArea.z * 5;
        player = GameObject.Find("Player").transform;
        MovePosition();
        GameObject canvas = GameObject.Find("Canvas");
        playerUIs = canvas.GetComponentsInChildren<IPlayerUI>().ToList();
        playerUIs?.ForEach(x => x.UpdateUI("CollectorTime", time));
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        playerUIs?.ForEach(x => x.UpdateUI("CollectorTime", time));

        if (time <= 0)
        {
            MovePosition();
            countOfCollecting++;
            time = 180;
        }
    }

    public void CollectItems(List<string> items)
    { 
        collectItems.AddRange(items);
        countOfCollecting++;
        MovePosition();
        time = 180;
        
        SummaryGame();
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

    private void MovePosition()
    {
        Vector3 position;

        if (player.position.x < 0 && 0 < player.position.z)
        { position = new Vector3(Random.Range(0, spawnArea_x), 0, Random.Range(-spawnArea_z, 0)); }
        else if (player.position.x < 0 && player.position.z < 0)
        { position = new Vector3(Random.Range(0, spawnArea_x), 0, Random.Range(0, spawnArea_z)); }
        else if (0 < player.position.x && 0 < player.position.z)
        { position = new Vector3(Random.Range(-spawnArea_x, 0), 0, Random.Range(-spawnArea_z, 0)); }
        else if (0 < player.position.x && player.position.z < 0)
        { position = new Vector3(Random.Range(-spawnArea_x, 0), 0, Random.Range(0, spawnArea_z)); }
        else
        { return; }

        transform.position = position;
    }
}
