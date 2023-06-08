using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamReceiver : MonoBehaviour
{
    [NonSerialized] public SpawnObjectList spawnEnemyList;
    [NonSerialized] public SpawnObjectList spawnItemList;
    [NonSerialized] public PlayerParam playerParam;
    [NonSerialized] public Dictionary<string, Vector2> mainObstacles;
}
