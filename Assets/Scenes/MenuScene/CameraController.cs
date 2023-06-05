using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {

    }

    public async Task<Task> MoveCamera(Vector3 position, Vector3 rotation, int frame)
    {
        float ratio = 1 / (float)frame;
        for (int i = 0; i < frame; i++)
        {
            transform.position = Vector3.Lerp(transform.position, position, ratio + (i * ratio));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation), ratio + (i * ratio));
            await Task.Delay((int)(Time.deltaTime * 1000f));
            if (transform.position == position && transform.rotation == Quaternion.Euler(rotation)) break;
        }
        return Task.CompletedTask;
    }
}
