using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.7f, 4.9f, 0.3f);
        transform.rotation = Quaternion.Euler(5, 50, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            StartCoroutine(RotateCamera(37.5f, -90f, 0));
        }
    }

    private IEnumerator RotateCamera(float x, float y, float z)
    {
        for (int i = 0; i < 1000; i++)
        {
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x, y, z), 0.01f);
            if (transform.rotation == Quaternion.Euler(x, y, z)) break;
        }
    }
}
