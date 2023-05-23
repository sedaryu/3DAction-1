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
    [SerializeField] private bool healable;
    public UnityAction onHealing;
    [SerializeField] private bool hittable;
    public UnityAction<Vector3, float> onHitting;
    public event Func<bool> isGroggy;
    [SerializeField] private bool grogable;
    public UnityAction<Smasher, float> onGrogging;
    [SerializeField] bool attackable;
    public event Func<float> onAttacking;

    void Update()
    {
        Heal();
    }

    public void Heal()
    {
        if (healable) onHealing.Invoke();
    }

    public void Hit(Vector3 vector, float attack)
    {
        if (hittable) onHitting.Invoke(vector, attack);
    }

    public bool IsGroggy { get => isGroggy.Invoke(); }

    public void Grog(Smasher smash, float time)
    {
        if (grogable) onGrogging.Invoke(smash, time);
        else Destroy(gameObject);
    }

    public float Attack()
    { 
        return attackable ? onAttacking.Invoke() : 0;
    }
}
