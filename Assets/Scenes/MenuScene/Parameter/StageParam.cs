using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageParam
{
    public StageType Type { get; private set; }
    public enum StageType
    { 
        Town,
        Forest,
    }
    public Dictionary<string, Vector2> Obstacles { get; private set; } = new Dictionary<string, Vector2>();
    public string[] SubObstacles { get; private set; }
    public SpawnObjectList SpawnEnemyList { get; private set; }
    public SpawnObjectList SpawnItemList { get; private set; }

    public StageParam(int stageType, SpawnObjectList enemyList, SpawnObjectList itemList)
    {
        Type = (StageType)Enum.ToObject(typeof(StageType), stageType);
        SpawnEnemyList = enemyList;
        SpawnItemList = itemList;
    }

    public void SetObstacles(List<string> obstaclesKey, List<Vector2> obstaclesValue, string[] subObstacles)
    {
        for (int i = 0; i < obstaclesKey.Count; i++)
        {
            Obstacles.Add(obstaclesKey[i], obstaclesValue[i]);
        }
    }

    public void SetObstacles(Dictionary<string, Vector2> obstacles, string[] subObstacles)
    { 
        Obstacles = obstacles;
        SubObstacles = subObstacles;
    }
}

[System.Serializable]
public class StageParamJson
{
    public int stageType;
    public List<string> obstaclesKey;
    public List<Vector2> obstaclesValue;
    public string[] subObstacles;
    public SpawnObjectList spawnEnemyList;
    public SpawnObjectList spawnItemList;

    public StageParamJson(int stageType, List<string> obstaclesKey, List<Vector2> obstaclesValue, string[] subObstacles, 
                          SpawnObjectList spawnEnemyList, SpawnObjectList spawnItemList)
    {
        this.stageType = stageType;
        this.obstaclesKey = obstaclesKey;
        this.obstaclesValue = obstaclesValue;
        this.subObstacles = subObstacles;
        this.spawnEnemyList = spawnEnemyList;
        this.spawnItemList = spawnItemList;
    }
}
