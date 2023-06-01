using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUIController : MonoBehaviour, IPlayerUI
{
    //テキストUI
    //private Text bulletText;
    private Text magazinText;

    void Awake()
    {
        //bulletText = transform.Find("Bullet").GetComponent<Text>();
        magazinText = transform.Find("MagazinText").GetComponent<Text>();
    }

    public void UpdateUI(string key, float value)
    {
        if (key != "Bullet") return;
        magazinText.text = ((int)value).ToString();
    }

    public void UpdateUI(string key, string value)
    {
    }

    //public void UpdateMagazinTextUI(string text)
    //{
    //    magazinText.text = text;
    //}

    //public void UpdateBulletTextUI(string text)
    //{
    //    //bulletText.text = text;
    //}
}
