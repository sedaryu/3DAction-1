using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static SpawnObjectList;
using static UnityEditor.Progress;

public class GameResulter : MonoBehaviour
{
    public List<string> collectItems;

    private Text itemsText;

    private CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        itemsText = GameObject.Find("Canvas").transform.Find("ItemsText").GetComponent<Text>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        Time.timeScale = 1;
        Task _ = ResultItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private async Task ResultItems()
    {
        string[] itemTypes = collectItems.Distinct().ToArray();
        List<int> itemCount = new List<int>();
        foreach (string type in itemTypes)
        { itemCount.Add(collectItems.Where(x => x == type).Count()); }

        GameObject[] spawnObjects = GameObject.Find("LoadAsset").GetComponent<LoadAsset>().LoadObjects("ResultItem", itemTypes);

        for (int i = 0; i < itemTypes.Length; i++)
        {
            for (int n = 0; n < itemCount[i]; n++)
            {
                Instantiate(spawnObjects[i], new Vector3(Random.Range(-0.75f, -0.65f), 5.0f, 0.2f + (0.4f * i)), Quaternion.identity);
                await Task.Delay(100);
            }
        }
        await Task.Delay(1750);

        Task task = await cameraController.MoveCamera(new Vector3(0.2f, 5.5f, 0.6f), new Vector3(37.5f, -90f, 0), 10000);

        for (int i = 0; i < itemTypes.Length; i++)
        {
            Task task1 = await cameraController.MoveCamera(new Vector3(0.2f, 5f, 0.2f + (0.4f * i)), new Vector3(26f, -90f, 0), 2500);
            itemsText.text = $"{itemTypes[i]} ";
            await Task.Delay(1000);
            itemsText.text += "* ";
            await Task.Delay(1000);
            itemsText.text += $"{itemCount[i]}";
            await Task.Delay(1000);
        }
    }
}
