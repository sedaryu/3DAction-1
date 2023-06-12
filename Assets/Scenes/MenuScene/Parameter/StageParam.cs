using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public StageParam(int stageType, List<string> enemyKey, List<int> enemyValue, List<string> itemKey, List<int> itemValue)
    {
        Type = (StageType)Enum.ToObject(typeof(StageType), stageType);
        SpawnEnemyList = new SpawnObjectList(1.5f, new SpawnObjectList.SpawnObject[3] 
        { new SpawnObjectList.SpawnObject(enemyKey[0], enemyValue[0]), 
          new SpawnObjectList.SpawnObject(enemyKey[1], enemyValue[1]), 
          new SpawnObjectList.SpawnObject(enemyKey[2], enemyValue[2]) });
        SpawnItemList = new SpawnObjectList(0, new SpawnObjectList.SpawnObject[3]
        { new SpawnObjectList.SpawnObject(itemKey[0], itemValue[0]),
          new SpawnObjectList.SpawnObject(itemKey[1], itemValue[1]),
          new SpawnObjectList.SpawnObject(itemKey[2], itemValue[2]) });
    }

    public void SetObstacles(List<string> obstaclesKey, List<Vector2> obstaclesValue, string[] subObstacles)
    {
        for (int i = 0; i < obstaclesKey.Count; i++)
        {
            Obstacles.Add(obstaclesKey[i], obstaclesValue[i]);
        }
        SubObstacles = subObstacles;
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
    public List<string> spawnEnemyKey;
    public List<int> spawnEnemyValue;
    public List<string> spawnItemKey;
    public List<int> spawnItemValue;

    public StageParamJson(StageParam param)
    {
        this.stageType = (int)param.Type;
        this.obstaclesKey = param.Obstacles.Keys.ToList();
        this.obstaclesValue = param.Obstacles.Values.ToList();
        this.subObstacles = param.SubObstacles;
        this.spawnEnemyKey = param.SpawnEnemyList.SpawnObjects.ToList().Select(x => x.name).ToList();
        this.spawnEnemyValue = param.SpawnEnemyList.SpawnObjects.ToList().Select(x => x.appearanceProbability).ToList();
        this.spawnItemKey = param.SpawnItemList.SpawnObjects.ToList().Select(x => x.name).ToList();
        this.spawnItemValue = param.SpawnItemList.SpawnObjects.ToList().Select(x => x.appearanceProbability).ToList();
    }
}
