using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageParam
{
    public StageType type;
    public enum StageType
    { 
        Town,
        Forest,
    }

    public SpawnObjectList spawnEnemyList;

    public SpawnObjectList spawnItemList;

    public StageParam(StageType stageType, SpawnObjectList enemyList, SpawnObjectList itemList)
    { 
        type = stageType;
        spawnEnemyList = enemyList;
        spawnItemList = itemList;
    }
}
