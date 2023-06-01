using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class LifeUIController : MonoBehaviour, IPlayerUI
{
    //テキストUI
    //private Text lifeMaxText;
    private Text lifeText;

    void Awake()
    {
        //lifeMaxText = transform.Find("LifeMax").GetComponent<Text>();
        lifeText = transform.Find("Life").GetComponent<Text>();
    }

    public void UpdateUI(string key, float value)
    {
        if (key != "Life") return;
        lifeText.text = ((int)value).ToString();
    }

    public void UpdateUI(string key, string value)
    {
    }

    //public void UpdateLifeMaxTextUI(string text)
    //{
    //    //lifeMaxText.text = text;
    //}

    //public void UpdateLifeTextUI(string text)
    //{
    //    lifeText.text = text;
    //}
}
