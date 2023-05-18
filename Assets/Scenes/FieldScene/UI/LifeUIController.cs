using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUIController : MonoBehaviour, ILifeUI
{
    //テキストUI
    private Text lifeMaxText;
    private Text lifeText;

    void Awake()
    {
        lifeMaxText = transform.Find("LifeMax").GetComponent<Text>();
        lifeText = transform.Find("Life").GetComponent<Text>();
    }

    public void UpdateLifeMaxTextUI(string text)
    {
        lifeMaxText.text = text;
    }

    public void UpdateLifeTextUI(string text)
    {
        lifeText.text = text;
    }
}
