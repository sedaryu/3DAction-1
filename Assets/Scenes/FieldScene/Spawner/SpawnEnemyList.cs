using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnEnemyList", menuName = "Custom/SpawnEnemyList")]
public class SpawnEnemyList : ScriptableObject
{
    public float SpawnTime => spawnTime;
    [SerializeField] private float spawnTime;

    public SpawnEnemy[] SpawnEnemies => spawnEnemies;
    [SerializeField] private SpawnEnemy[] spawnEnemies;

    [System.Serializable]
    public class SpawnEnemy
    {
        public string name;
        public int appearanceProbability = 0;
    }
}
