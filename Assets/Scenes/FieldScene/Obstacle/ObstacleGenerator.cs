using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private void Start()
    {
        Dictionary<string, Vector2> mainObstacles = GameObject.Find("ParamReceiver").GetComponent<ParamReceiver>().mainObstacles;

        GameObject[] obstacleObject = new LoadAsset().LoadObjects("Stage/Town", mainObstacles.Keys.ToArray());
        for (int i = 0; i < mainObstacles.Count; i++)
        {
            string key = mainObstacles.Keys.ToArray()[i];
            Vector3 position = new Vector3(mainObstacles[key].x, 0, mainObstacles[key].y);
            Instantiate(obstacleObject[i], position, Quaternion.Euler(0, 90, 0));
        }
    }
}
