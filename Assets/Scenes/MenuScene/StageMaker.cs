using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageMaker
{
    private bool[,] stageRange = new bool[56, 56];

    public Dictionary<string, Vector2> MakeStage(out string[] subObstacles)
    {
        Dictionary<string, Vector2> obstacles = new Dictionary<string, Vector2>()
        { { "Cafe", new Vector2(19, 13) }, { "FlowerShop", new Vector2(17, 13) }, 
          { "Garage", new Vector2(13, 21) }, { "Restaurant", new Vector2(25, 9) } };

        subObstacles = new string[13] { "Bushs", "Bush", "Bush", "Bush", "Bush", "Bush", "Pallet", "Pallet", "Pallet", "Pallet", "Pallet", "Pallet", "Pallet" };

        int[] random = new int[4] { 0, 1, 2, 3 };

        for (int i = 0; i < 50; i++)
        {
            int a = Random.Range(0, 4); int b = Random.Range(0, 4);
            (random[a], random[b]) = (random[b], random[a]);
        }

        for (int i = 0; i < 4; i++)
        {

            int x = (int)obstacles.Values.ToArray()[random[i]].x;
            int y = (int)obstacles.Values.ToArray()[random[i]].y;
            int a; int b;
            if (i == 0) { a = Random.Range(0, 28 - x); b = Random.Range(0, 28 - y); }
            else if (i == 1) { a = Random.Range(28, 56 - x); b = Random.Range(0, 28 - y); }
            else if (i == 2) { a = Random.Range(0, 28 - x); b = Random.Range(28, 56 - y); }
            else { a = Random.Range(28, 56 - x); b = Random.Range(28, 56 - y); }

            obstacles[obstacles.Keys.ToArray()[random[i]]] = new Vector2((a + x * 0.5f) * 0.25f - 7, (b + y * 0.5f) * 0.25f - 7);
        }
        return obstacles;
    }
}
