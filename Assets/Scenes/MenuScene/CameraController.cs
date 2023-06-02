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
        transform.position = new Vector3(0.7f, 4.9f, 0.3f);
        transform.rotation = Quaternion.Euler(5, 50, 0);
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

    //1.5 5.2 -1.5  6.5 -40 0
}
