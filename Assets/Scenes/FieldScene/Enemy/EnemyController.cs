using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour, ITargetable, IGrogable, IAttackable
{
    public UnityAction onHealing;

    public UnityAction<Vector3, float> onHitting;

    public event Func<bool> isGroggy;
    public UnityAction<Smasher, float> onGrogging;

    public event Func<float> onAttacking;

    void Update()
    {
        Heal();
    }

    public void Heal()
    { 
        onHealing.Invoke();
    }

    public void Hit(Vector3 vector, float attack)
    {
        onHitting.Invoke(vector, attack);
    }

    public bool Groggy { get => isGroggy.Invoke(); }

    public void Grog(Smasher smash, float time)
    { 
        onGrogging.Invoke(smash, time);
    }

    public float Attack()
    { 
        return onAttacking.Invoke();
    }
}
