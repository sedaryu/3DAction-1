using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public ScriptableObject[] LoadParam(string type, string[] names)
    {
        List<ScriptableObject> objects = new List<ScriptableObject>();
        foreach (string name in names)
        {
            objects.Add(AssetDatabase.LoadAssetAtPath<ScriptableObject>($"Assets/ScriptableObject/{type}/{name}.asset") as ScriptableObject);
        }
        return objects.ToArray();
    }

    public ScriptableObject[] LoadParamsAll(string type)
    {
        List<ScriptableObject> objects = new List<ScriptableObject>();
        string[] paths = Directory.GetFiles($"Assets/ScriptableObject/{type}").ToList().Where(x => !x.Contains(".meta")).ToArray();
        foreach (string path in paths) 
        {
            objects.Add(AssetDatabase.LoadAssetAtPath<ScriptableObject>(path) as ScriptableObject);
        }
        return objects.ToArray();
    }

    public GameObject[] LoadObjectsAll(string type)
    {
        List<GameObject> objects = new List<GameObject>();
        string[] paths = Directory.GetFiles($"Assets/Prefab/{type}").ToList().Where(x => !x.Contains(".meta")).ToArray();
        foreach (string path in paths)
        {
            objects.Add(AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject);
        }
        return objects.ToArray();
    }
}
