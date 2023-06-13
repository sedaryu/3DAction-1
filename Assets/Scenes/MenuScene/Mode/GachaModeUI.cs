using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class GachaModeUI : SelectModeUI
{
    [SerializeField] private Collider gachaBox;
    private BackUI backUI;

    private MenuParam param;

    private void Awake()
    {
        colliders.Add(gachaBox);
        backUI = transform.parent.transform.Find("Back").GetComponent<BackUI>();
        onClicked += () =>
        {
            headlineText.rectTransform.sizeDelta = new Vector2(150, 50);
            headlineText.rectTransform.anchoredPosition = new Vector3(-5, 5, 0);
            sublineText.fontSize = 7;
            sublineText.rectTransform.sizeDelta = new Vector2(350, 70);
            sublineText.rectTransform.anchoredPosition = new Vector3(5, 5, 0);
            param = new LoadJson().LoadMenuParam();
            initialText = $"Clicking GachaBox,\nYou Can Try Rarity Gacha\nGachaRarity: {param.Parameter("Random")}%";
        };
    }

    public async void OnClickGachaBox()
    {
        if (param.Parameter("Random") == 0) { sublineText.text = "Gacha Rarity Is 0%,\nYou Cannot Try To Gacha"; return; }
        gachaBox.enabled = false;
        backUI.SetInvisible();
        Transform top = gachaBox.transform.Find("ToolBoxTop");
        headlineText.rectTransform.sizeDelta = new Vector2(500, 100);
        headlineText.rectTransform.anchoredPosition = new Vector3(-5, 190, 0);
        headlineText.text = "";
        string text;

        if (Random.Range(1, 101) <= param.Parameter("Random"))
        {
            string[] paths = Directory.GetFiles($"Assets/Prefab/Gun").ToList().Where(x => !x.Contains(".meta")).ToArray();
            string name = paths[Random.Range(0, paths.Length)].Split("\\")[1].Split(".")[0];
            text = $"{name} Get!!!";
            GameObject gun = new LoadAsset().LoadObject<GameObject>("Gun", name);
            Instantiate(gun, new Vector3(-1.7f, 5.15f, 0.65f), Quaternion.Euler(0, 0, 90));
            EquipmentGunParam equip = new LoadJson().LoadEquipmentGunParam();
            if (equip.guns.Count >= 6) equip.guns[5] = gun.name;
            else equip.guns.Add(gun.name);
            new LoadJson().SaveEquipmentGunParam(equip);
        }
        else
        {
            int point = Random.Range(500, 2501);
            text = $"{point} Point Get!!!";
            GameObject gun = new LoadAsset().LoadObject<GameObject>("ResultItem", "Money");
            Instantiate(gun, new Vector3(-1.7f, 5.15f, 0.65f), Quaternion.Euler(0, 90, 0));
            MenuParam menu = new LoadJson().LoadMenuParam();
            menu.ChangeParameter("Point", point);
            new LoadJson().SaveMenuParam(menu);
        }
        Task open = await OpenBox(top);
        Task task = await cameraController.MoveCamera(new Vector3(-1.55f, 5.75f, 0.65f), new Vector3(70, -90, 0), 3000);
        headlineText.text = text;
        MenuParam menuParam = new LoadJson().LoadMenuParam();
        menuParam.SetParameter("Random", 0);
        new LoadJson().SaveMenuParam(menuParam);
        Task wait = await Task.Run(async () => { await Task.Delay(3000); return Task.CompletedTask; } );
        top.rotation *= Quaternion.Euler(10, 0, 0);
        backUI.BackToMainMode();
    }

    private async Task<Task> OpenBox(Transform top)
    {
        for (int i = 0; i < 50; i++)
        {
            await Task.Delay(25);
            top.rotation *= Quaternion.Euler(-0.2f, 0, 0); 
        }
        return Task.CompletedTask;
    }
}
