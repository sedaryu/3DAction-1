using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static SpawnObjectList;
using static UnityEditor.Progress;

public class GameResulter : MonoBehaviour
{
    public bool isResultable = false;

    public List<ItemParam> collectItems;
    public float life;

    private Text headlineText;

    private CameraController cameraController;

    // Start is called before the first frame update
    void Awake()
    {
        headlineText = GameObject.Find("Canvas").transform.Find("HeadlineText").GetComponent<Text>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    
    public async Task<Task> ResultItems()
    {
        if (!isResultable) { Debug.Log("Skip"); return Task.CompletedTask; }

        headlineText.text = "StageClear";
        cameraController.transform.position = new Vector3(0.7f, 4.9f, 0.3f);
        cameraController.transform.rotation = Quaternion.Euler(5, 50, 0);

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

        Task task = await cameraController.MoveCamera(new Vector3(0.2f, 5.5f, 0.6f), new Vector3(37.5f, -90f, 0), 3000);

        MenuParam param = new LoadJson().LoadMenuParam();
        param.SetParameter("Life", life);

        for (int i = 0; i < items.Length; i++)
        {
            Task task1 = await cameraController.MoveCamera(new Vector3(0.2f, 5f, 0.2f + (0.4f * i)), new Vector3(26f, -90f, 0), 500);
            headlineText.text = $"{items[i].Name} ";
            await Task.Delay(1000);
            headlineText.text += "* ";
            await Task.Delay(1000);
            headlineText.text += $"{itemCount[i]} ";
            await Task.Delay(1000);
            headlineText.text += $"\n= ";
            await Task.Delay(1000);
            int value = (int)(itemCount[i] * items[i].Unique);
            param.ChangeParameter(items[i].Type.ToString(), value);
            headlineText.text += $"{value} {items[i].Text}";
            await Task.Delay(2500);
        }

        new LoadJson().SaveMenuParam(param);

        headlineText.text = "";

        return Task.CompletedTask;
    }
}
