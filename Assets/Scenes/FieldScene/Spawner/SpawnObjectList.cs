using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SpawnObjectList", menuName = "Custom/SpawnObjectList")]
public class SpawnObjectList : ScriptableObject
{
    public float SpawnTime => spawnTime;
    [SerializeField] private float spawnTime;

    public SpawnObject[] SpawnObjects => spawnObjects;
    [SerializeField] private SpawnObject[] spawnObjects;

    public SpawnObjectList(float time, SpawnObject[] objects)
    { 
        spawnTime = time;
        spawnObjects = objects;
    }

    [System.Serializable]
    public class SpawnObject
    {
        public string name;
        public int appearanceProbability = 0;

        public SpawnObject(string _name, int _appear)
        { 
            name = _name;
            appearanceProbability = _appear;
        }
    }
}
