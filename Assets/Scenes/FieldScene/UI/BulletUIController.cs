using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUIController : MonoBehaviour
{
    //テキストUI
    private Text bulletText;
    private Text magazinText;

    void Awake()
    {
        bulletText = transform.Find("Bullet").GetComponent<Text>();
        magazinText = transform.Find("Magazin").GetComponent<Text>();
    }

    public void UpdateBulletUI(string bullet)
    {
        magazinText.text = bullet;
    }
}
