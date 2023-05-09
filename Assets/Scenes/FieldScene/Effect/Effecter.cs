using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Effecter", menuName = "Custom/Effecter")]
public class Effecter : ScriptableObject
{
    public List<Effect> Effects
    {
        get => effects;
    }
    [SerializeField] private List<Effect> effects = new List<Effect>();

    [System.Serializable]
    public class Effect
    {
        public string key;
        public GameObject value;
    }
}
