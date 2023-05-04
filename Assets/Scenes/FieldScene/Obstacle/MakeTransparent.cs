using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material initialMaterial;
    [SerializeField] private Material transparentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initialMaterial = meshRenderer.material;
    }

    public void Transparent()
    {
        meshRenderer.material = transparentMaterial;
    }

    public void CancelTransparent()
    {
        meshRenderer.material = initialMaterial;
    }
}
