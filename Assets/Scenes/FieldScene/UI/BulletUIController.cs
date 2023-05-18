using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUIController : MonoBehaviour, IBulletUI
{
    //テキストUI
    private Text bulletText;
    private Text magazinText;

    void Awake()
    {
        bulletText = transform.Find("Bullet").GetComponent<Text>();
        magazinText = transform.Find("Magazin").GetComponent<Text>();
    }

    public void UpdateMagazinTextUI(string text)
    {
        magazinText.text = text;
    }

    public void UpdateBulletTextUI(string text)
    {
        bulletText.text = text;
    }
}
