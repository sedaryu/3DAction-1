using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamReceiver : MonoBehaviour
{
    public SpawnObjectList SpawnEnemyList => stageParam.SpawnEnemyList;
    public SpawnObjectList SpawnItemList => stageParam.SpawnItemList;
    public Dictionary<string, Vector2> Obstacles => stageParam.Obstacles;
    public string[] SubObstacles => stageParam.SubObstacles;
    public PlayerParam PlayerParam => playerParam;

    [NonSerialized] public StageParam stageParam;
    [NonSerialized] public PlayerParam playerParam;

    private void Start()
    {
        GameObject.Find("GameController").GetComponent<GameController>().stageParam = stageParam;
    }
}
