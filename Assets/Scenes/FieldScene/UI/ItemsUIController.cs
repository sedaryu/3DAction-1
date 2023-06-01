using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUIController : MonoBehaviour, IPlayerUI
{
    private Text itemsText;

    void Awake()
    {
        itemsText = transform.Find("ItemsText").GetComponent<Text>();
    }

    public void UpdateUI(string key, float value)
    {
    }

    public void UpdateUI(string key, string value)
    {
        if (key != "Items") return;
        itemsText.text = value;
    }
}
