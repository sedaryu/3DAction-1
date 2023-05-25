using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : MonoBehaviour
{
    public ShoesParam Param { get => param; }

    [SerializeField] private ShoesParam param;
}
