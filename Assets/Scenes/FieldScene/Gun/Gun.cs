using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunParam Param { get => param; }

    [SerializeField] private GunParam param;
}
