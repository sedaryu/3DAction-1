using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

public class EnemyDamager : MonoBehaviour, ITargetable
{
    public UnityAction<Vector3, float> onHitting;

    public void Hit(Vector3 vector, float attack)
    { 
        onHitting.Invoke(vector, attack);
    }
}
