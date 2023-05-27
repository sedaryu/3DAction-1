using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnObjectList", menuName = "Custom/SpawnObjectList")]
public class SpawnObjectList : ScriptableObject
{
    public float SpawnTime => spawnTime;
    [SerializeField] private float spawnTime;

    public SpawnObject[] SpawnObjects => spawnObjects;
    [SerializeField] private SpawnObject[] spawnObjects;

    [System.Serializable]
    public class SpawnObject
    {
        public string name;
        public int appearanceProbability = 0;
    }
}
