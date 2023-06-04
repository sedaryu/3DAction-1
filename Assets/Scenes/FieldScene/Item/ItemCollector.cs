using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ItemCollector : MonoBehaviour, IParametable
{
    [SerializeField] private int collectCount;
    [SerializeField] private float timeCount;

    private List<ItemParam> collectItems = new List<ItemParam>();
    private int countOfCollecting = 0;
    private float time;

    private Vector3 spawnArea;
    private float spawnArea_x;
    private float spawnArea_z;

    private Transform player;

    private List<IPlayerUI> playerUIs;
    private GameController gameController;

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
        time = timeCount;
        playerUIs?.ForEach(x => x.UpdateUI("CollectorTime", time));

        SetEventOnGameClear();
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
            time = timeCount;
            SummaryGame();
        }
    }

    public void CollectItems(List<ItemParam> items)
    { 
        collectItems.AddRange(items);
        countOfCollecting++;
        MovePosition();
        time = timeCount;
        
        SummaryGame();
    }

    private void SummaryGame()
    {
        if (collectCount <= countOfCollecting) 
        {
            gameController.JudgeGameClear(GameController.ClearConditions.ItemCollected);
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

    public void SetEventOnGameClear()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.onGameClear += SetParamToNextScene;
    }

    public void SetParamToNextScene()
    {
        gameController.collectItems = collectItems;
    }
}
