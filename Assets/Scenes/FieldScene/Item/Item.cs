using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IGatherable
{
    [SerializeField] private string type;
    [SerializeField] private float weight;

    public string Gathered(out float _weight)
    { 
        _weight = weight;
        return type;
    }

    public void Disappear()
    { 
        Destroy(gameObject);
    }
}
