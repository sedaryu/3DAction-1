using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalineUIController : MonoBehaviour, IPlayerUI
{
    private Image adrenalineImage;
    private Text adrenalineTankText;

    void Awake()
    {
        adrenalineImage = transform.Find("AdrenalineImage").GetComponent<Image>();
        adrenalineTankText = transform.Find("AdrenalineTankText").GetComponent<Text>();
    }

    public void UpdateUI(string key, float value)
    {
        if (key == "Adrenaline") adrenalineImage.fillAmount = value;
        else if (key == "AdrenalineTank") adrenalineTankText.text = ((int)value).ToString();
    }

    //public void UpdateAdrenalineUI(float adre)
    //{ 
    //    adrenalineImage.fillAmount = adre;
    //}

    //public void UpdateAdrenalineTankUI(int tank)
    //{
    //    adrenalineTankText.text = tank.ToString();
    //}
}
