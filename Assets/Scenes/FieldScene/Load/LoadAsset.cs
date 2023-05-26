using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LoadAsset : MonoBehaviour
{
    public GameObject[] LoadObjects(string[] paths)
    { 
        List<GameObject> objects = new List<GameObject>();
        foreach (string path in paths) 
        {
            objects.Add(AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefab/Enemy/{path}.prefab") as GameObject);
        }
        return objects.ToArray();
    }
}
