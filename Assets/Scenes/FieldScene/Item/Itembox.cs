using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itembox : MonoBehaviour, IGatherable
{
    [SerializeField] private string type;
    [SerializeField] private float weight;

    public string Gathered(out float _weight)
    {
        GetComponent<Collider>().enabled = false;
        _weight = weight;
        Disappear();
        return type;
    }

    public void Disappear()
    { 
        Destroy(gameObject);
    }
}
