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
    public List<ItemParam> collectItems;

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
        ItemParam[] items = collectItems.Distinct().ToArray();
        List<int> itemCount = new List<int>();
        foreach (ItemParam item in items)
        { itemCount.Add(collectItems.Where(x => x.Name == item.Name).Count()); }

        GameObject[] spawnObjects = GameObject.Find("LoadAsset").GetComponent<LoadAsset>()
                                    .LoadObjects("ResultItem", items.Select(x => x.Name).ToArray());

        for (int i = 0; i < items.Length; i++)
        {
            for (int n = 0; n < itemCount[i]; n++)
            {
                Instantiate(spawnObjects[i], new Vector3(Random.Range(-0.75f, -0.65f), 5.0f, 0.2f + (0.4f * i)), Quaternion.identity);
                await Task.Delay(100);
            }
        }
        await Task.Delay(1750);

        Task task = await cameraController.MoveCamera(new Vector3(0.2f, 5.5f, 0.6f), new Vector3(37.5f, -90f, 0), 10000);

        for (int i = 0; i < items.Length; i++)
        {
            Task task1 = await cameraController.MoveCamera(new Vector3(0.2f, 5f, 0.2f + (0.4f * i)), new Vector3(26f, -90f, 0), 2500);
            itemsText.text = $"{items[i].Name} ";
            await Task.Delay(1000);
            itemsText.text += "* ";
            await Task.Delay(1000);
            itemsText.text += $"{itemCount[i]} ";
            await Task.Delay(1000);
            itemsText.text += $"= ";
            await Task.Delay(1000);
            itemsText.text += $"{itemCount[i] * items[i].Unique} {items[i].Text}";
            await Task.Delay(2500);
        }
        itemsText.text = "";
        Task task2 = await cameraController.MoveCamera(new Vector3(1.5f, 5.2f, -1.5f), new Vector3(6.5f, -40, 0), 10000);
    }
}
