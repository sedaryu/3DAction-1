using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetecter : MonoBehaviour
{
    private MeshCollider meshCollider;

    // Start is called before the first frame update
    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = GetComponent<MeshFilter>().sharedMesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{ Debug.Log("Target!!"); }
    }
}
