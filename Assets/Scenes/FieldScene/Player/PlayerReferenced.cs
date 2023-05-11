using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferenced : MonoBehaviour
{
    public event Action<float> onTriggerAttacked;

    public void OnAttacked(float damage)
    {
        onTriggerAttacked.Invoke(damage);
    }
}
