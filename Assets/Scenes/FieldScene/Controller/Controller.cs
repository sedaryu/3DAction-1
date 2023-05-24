using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Controller : MonoBehaviour
{
    public UnityAction<Vector3> onMoving;
    public UnityAction<Vector3> onLooking;
    public UnityAction onFiring;
    public UnityAction onReloading;
    public UnityAction onSmashing;
    public UnityAction onBursting;
    public UnityAction onDecreasingAdrenaline;

    public abstract void InputMoving();

    public abstract void InputFiring();

    public abstract void InputReloading();

    public abstract void InputSmashing();

    public abstract void InputLooking();

    public abstract void InputBursting();
}
