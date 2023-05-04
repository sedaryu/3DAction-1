using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Effecter : MonoBehaviour
{
    [SerializeField] private List<Effect> effects = new List<Effect>();

    public GameObject GetEffectFromKey(string key)
    {
        foreach (Effect x in effects)
        {
            if (x.key == key)
            {
                return x.value;
            }
        }
        return null;
    }

    [System.Serializable]
    public class Effect
    {
        public string key;
        public GameObject value;
    }
}
