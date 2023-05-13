using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    public Transform Transform { get; }

    public void Hit(Vector3 vector, float attack);
}
