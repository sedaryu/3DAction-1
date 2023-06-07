using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMode : MonoBehaviour
{
    //output
    private Text headlineText;
    private Text sublineText;

    //input
    private List<SelectModeUI> modeUIs;
    private BackUI backUI;

    //camera
    private CameraController cameraController;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        headlineText = canvas.transform.Find("HeadlineText").GetComponent<Text>();
        sublineText = canvas.transform.Find("SublineText").GetComponent<Text>();

        backUI = canvas.transform.Find("Back").GetComponent<BackUI>();
        backUI.onBack += BackToMainMenu;
        modeUIs = canvas.GetComponentsInChildren<SelectModeUI>().ToList();
        modeUIs.ForEach(x => x.onClicking += () => modeUIs.ForEach(x => x.SetInvisible()));
        modeUIs.ForEach(x => x.onClicked += backUI.SetVisible);

        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    public async Task<Task> SetMainMode()
    {
        headlineText.enabled = false;
        sublineText.enabled = false;
        Task task = await cameraController.MoveCamera(new Vector3(1.5f, 5.2f, -1.5f), new Vector3(6.5f, -40, 0), 3000);
        headlineText.enabled = true;
        sublineText.enabled = true;

        MenuParam param = new LoadJson().LoadMenuParam();
        headlineText.rectTransform.sizeDelta = new Vector2(150, 50);
        headlineText.rectTransform.anchoredPosition = new Vector3(-5, 5, 0);
        headlineText.text = $"Days {param.Parameter("Day").ToString()}";
        sublineText.rectTransform.sizeDelta = new Vector2(350, 70);
        sublineText.rectTransform.anchoredPosition = new Vector3(5, 5, 0);
        sublineText.text = 
        $"Point: {param.Parameter("Point")}\n" +
        $"Life: {param.Parameter("Life")}/{param.Parameter("LifeMax")}\n" +
        $"GachaRarity: {param.Parameter("Random")}%";

        modeUIs.ForEach(x => x.SetVisible());

        return Task.CompletedTask;
    }

    public async void BackToMainMenu()
    {
        modeUIs.ForEach(x => x.colliders.ForEach(x => x.enabled = false));
        Task task = await SetMainMode();
    }
}
