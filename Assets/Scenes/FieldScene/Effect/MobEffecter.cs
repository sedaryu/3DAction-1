using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Effecter;

public class MobEffecter : MonoBehaviour
{
    //エフェクター
    public Effecter Effecter
    {
        get => _effecter;
    }
    [SerializeField] private Effecter _effecter;

    public GameObject InstanceEffect(string key)
    {
        GameObject effect = Effecter.Effects.Find(x => x.key == key).value;
        if (effect == null) return null;
        return Instantiate(effect, transform.position, transform.rotation);
    }

    public GameObject InstanceEffect(string key, Vector3 position, Quaternion rotation)
    {
        GameObject effect = Effecter.Effects.Find(x => x.key == key).value;
        if (effect == null) return null;
        return Instantiate(effect, position, rotation);
    }

    public GameObject InstanceEffect(GameObject effect)
    {
        return Instantiate(effect, transform.position, transform.rotation);
    }

    public GameObject InstanceEffect(GameObject effect, Vector3 position, Quaternion rotation)
    {
        return Instantiate(effect, position, rotation);
    }
}
