using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    public bool IsGroggy { get; }

    public bool IsDestroyed { get; }

    public void Hit(Vector3 vector, float attack);

    public void Critical();

    public void Die();
}
