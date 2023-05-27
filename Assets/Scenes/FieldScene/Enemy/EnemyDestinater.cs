using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestinater : MonoBehaviour
{
    public UnityAction<Vector3> onMoving;

    private Transform destination;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        DetectDestibation(GameObject.Find("Player").transform);
    }

    // Update is called once per frame
    void Update()
    {
        InputMoving();
    }

    public void DetectDestibation(Transform dest)
    { 
        destination = dest;
    }

    public void InputMoving()
    {
        if (destination != null) onMoving.Invoke(destination.position);
    }
}
