using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectorUIController : MonoBehaviour, IPlayerUI
{
    //ƒ^ƒCƒ€UI
    private Text timeText;

    void Awake()
    {
        timeText = transform.Find("CollectorTimeText").GetComponent<Text>();
    }

    public void UpdateUI(string key, float value)
    {
        if (key != "CollectorTime") return;
        timeText.text = ((int)value).ToString();
    }
}
