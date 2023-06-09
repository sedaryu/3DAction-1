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
    public Dictionary<string, Vector2> Obstacles { get; private set; }
    public string[] SubObstacles { get; private set; }
    public SpawnObjectList SpawnEnemyList { get; private set; }
    public SpawnObjectList SpawnItemList { get; private set; }

    public StageParam(StageType stageType, SpawnObjectList enemyList, SpawnObjectList itemList)
    { 
        Type = stageType;
        SpawnEnemyList = enemyList;
        SpawnItemList = itemList;
    }

    public void SetObstacles(Dictionary<string, Vector2> obstacles, string[] subObstacles)
    { 
        Obstacles = obstacles;
        SubObstacles = subObstacles;
    }
}
