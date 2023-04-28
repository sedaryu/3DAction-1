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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
