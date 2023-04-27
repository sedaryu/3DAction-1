using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Color32 initialColor;
    private Color32 transparentColor;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initialColor = meshRenderer.material.color;
        transparentColor = new Color32(Convert.ToByte((int)meshRenderer.material.color.r * 255),
                                       Convert.ToByte((int)meshRenderer.material.color.g * 255),
                                       Convert.ToByte((int)meshRenderer.material.color.b * 255), 60);
    }

    public void Transparent()
    {
        meshRenderer.material.color = transparentColor;
    }

    public void CancelTransparent()
    {
        meshRenderer.material.color = initialColor;
    }
}
