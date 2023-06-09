using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private Vector3 spawnArea;
    private float spawnArea_x;
    private float spawnArea_z;

    private void Start()
    {
        ParamReceiver paramReceiver = GameObject.Find("ParamReceiver").GetComponent<ParamReceiver>();
        Dictionary<string, Vector2> mainObstacles = paramReceiver.Obstacles;

        GameObject[] obstacleObject = new LoadAsset().LoadObjects("Stage/Town", mainObstacles.Keys.ToArray());
        for (int i = 0; i < mainObstacles.Count; i++)
        {
            string key = mainObstacles.Keys.ToArray()[i];
            Vector3 position = new Vector3(mainObstacles[key].x, 0, mainObstacles[key].y);
            Instantiate(obstacleObject[i], position, Quaternion.Euler(0, 90, 0));
        }

        spawnArea = GameObject.Find("BakedPlane").transform.localScale;
        spawnArea_x = spawnArea.x * 5;
        spawnArea_z = spawnArea.z * 5;

        string[] subObstacles = paramReceiver.SubObstacles;
        GameObject[] subObstacleObject = new LoadAsset().LoadObjects("Stage/Town", subObstacles);
        for (int i = 0; i < subObstacles.Length; i++)
        {
            Instantiate(subObstacleObject[i], new Vector3(Random.Range(-spawnArea_x, spawnArea_x), 0, Random.Range(-spawnArea_z, spawnArea_z)), Quaternion.identity);
        }
    }
}
