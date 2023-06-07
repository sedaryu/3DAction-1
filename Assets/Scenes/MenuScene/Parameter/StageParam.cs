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
    public SpawnObjectList SpawnEnemyList { get; private set; }
    public SpawnObjectList SpawnItemList { get; private set; }

    public StageParam(StageType stageType, SpawnObjectList enemyList, SpawnObjectList itemList)
    { 
        Type = stageType;
        SpawnEnemyList = enemyList;
        SpawnItemList = itemList;
    }
}
