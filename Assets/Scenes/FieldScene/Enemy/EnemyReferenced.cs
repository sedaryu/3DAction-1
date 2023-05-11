using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReferenced : MonoBehaviour
{
    public event Action<Vector3, float> onTriggerAttacked;

    public void OnAttacked(Vector3 vector, float damage)
    {
        onTriggerAttacked.Invoke(vector, damage);
    }
}
