using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilledCircleController : MonoBehaviour
{
    //Fill
    private GameObject fill;

    void Awake()
    {
        fill = transform.Find("HitPointCircleFill").gameObject;
    }

    public void UpdateFill(float hp)
    {
        fill.transform.localScale = new Vector2(1 - hp, 1 - hp);
    }
}
