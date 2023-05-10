using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUIController : MonoBehaviour
{
    //�A�N�g
    private PlayerAct act;
    //�e�L�X�gUI
    private Text bulletText;
    private Text magazinText;

    void Awake()
    {
        act = GameObject.Find("Player").GetComponent<PlayerAct>();
        bulletText = transform.Find("Bullet").GetComponent<Text>();
        magazinText = transform.Find("Magazin").GetComponent<Text>();
        act.onFiring += UpdateBulletUI;
        act.onReloading += UpdateBulletUI;
    }

    private void UpdateBulletUI(string bullet)
    {
        magazinText.text = bullet;
    }
}
