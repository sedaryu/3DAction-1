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
        StartCoroutine(OpenBox());
        _weight = weight;
        return type;
    }

    private IEnumerator OpenBox()
    {
        Transform top = transform.Find("ItemBoxTop");
        for (int i = 0; i < 0.75f; i++)
        {
            top.position += new Vector3(0, 0.02f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        Disappear();
    }

    public void Disappear()
    { 
        Destroy(gameObject);
    }
}
