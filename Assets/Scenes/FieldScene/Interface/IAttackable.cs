using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public string AttackKey { get; }
    public float Attack();
}
