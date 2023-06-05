using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Text headlineText;
    private Text sublineText;
    private List<SelectActionUI> modeUIs;
    private BackUI backUI;
    private CameraController cameraController;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        headlineText = canvas.transform.Find("HeadlineText").GetComponent<Text>();
        sublineText = canvas.transform.Find("SublineText").GetComponent<Text>();
        backUI = canvas.transform.Find("Back").GetComponent<BackUI>();
        backUI.onBack += BackToMainMenu;
        modeUIs = canvas.GetComponentsInChildren<SelectActionUI>().ToList();
        modeUIs.ForEach(x => x.onClicking += SetUIsInvisible);
        modeUIs.ForEach(x => x.onClicked += backUI.SetVisible);
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void SetUIsInvisible()
    {
        modeUIs.ForEach(x => x.SetInvisible());
    }

    public async Task<Task> SetMainMenu()
    {
        Task task = await cameraController.MoveCamera(new Vector3(1.5f, 5.2f, -1.5f), new Vector3(6.5f, -40, 0), 3000);

        MenuParam param = new LoadJson().LoadMenuParam();
        headlineText.text = $"Days {param.Parameter("Day").ToString()}";
        sublineText.text = 
        $"Point: {param.Parameter("Point")}\n" +
        $"Life: {param.Parameter("Life")}/{param.Parameter("LifeMax")}\n" +
        $"GachaRarity: {param.Parameter("Random")}%";

        modeUIs.ForEach(x => x.SetVisible());

        return Task.CompletedTask;
    }

    public async void BackToMainMenu()
    {
        Task task = await cameraController.MoveCamera(new Vector3(1.5f, 5.2f, -1.5f), new Vector3(6.5f, -40, 0), 3000);
        modeUIs.ForEach(x => x.SetVisible());
    }
}
