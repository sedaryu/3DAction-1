using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LoadAsset : MonoBehaviour
{
    public GameObject[] LoadObjects(string type, string[] names)
    { 
        List<GameObject> objects = new List<GameObject>();
        foreach (string name in names) 
        {
            objects.Add(AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefab/{type}/{name}.prefab") as GameObject);
        }
        return objects.ToArray();
    }
}
