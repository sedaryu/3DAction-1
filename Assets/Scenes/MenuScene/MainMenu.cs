using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Text headlineText;
    private Text sublineText;
    private CameraController cameraController;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        headlineText = canvas.transform.Find("HeadlineText").GetComponent<Text>();
        sublineText = canvas.transform.Find("SublineText").GetComponent<Text>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    public void SetMainMenu()
    {
        MenuParam param = new LoadJson().LoadMenuParam();
        headlineText.text = $"Days {param.Parameter("Day").ToString()}";
        sublineText.text = 
        $"Point: {param.Parameter("Point")}\n" +
        $"Life: {param.Parameter("Life")}/{param.Parameter("LifeMax")}\n" +
        $"GachaRarity: {param.Parameter("Random")}%";
    }
}
