using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetecter : MonoBehaviour
{
    public event Action<Collider> onTriggerEnter;
    public event Action<Collider> onTriggerStay;
    public event Action<Collider> onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (onTriggerEnter == null) return;
        onTriggerEnter.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (onTriggerStay == null) return;
        onTriggerStay.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (onTriggerExit == null) return;
        onTriggerExit.Invoke(other);
    }
}
