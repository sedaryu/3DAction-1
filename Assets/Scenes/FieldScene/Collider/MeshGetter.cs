using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGetter : MonoBehaviour
{
    private SkinnedMeshRenderer skinRenderer;

    private void Awake()
    {
        skinRenderer = transform.parent.Find("body").gameObject.GetComponent<SkinnedMeshRenderer>();
        GetComponent<MeshFilter>().sharedMesh = skinRenderer.sharedMesh;
    }
}
