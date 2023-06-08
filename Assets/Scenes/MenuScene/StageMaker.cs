using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageMaker
{
    public Dictionary<string, Vector2> MakeStage()
    {
        bool[,] stageRange = new bool[56,56];

        Dictionary<string, Vector2> mainObstacles = new Dictionary<string, Vector2>()
        { { "Cafe", new Vector2(19, 13) }, { "FlowerShop", new Vector2(17, 13) }, { "Garage", new Vector2(13, 21) },
          { "Restaurant", new Vector2(25, 9) } };

        int[] random = new int[4] { 0, 1, 2, 3 };

        for (int i = 0; i < 50; i++)
        {
            int a = Random.Range(0, 4); int b = Random.Range(0, 4);
            (random[a], random[b]) = (random[b], random[a]);
        }

        for (int i = 0; i < 4; i++)
        {

            int x = (int)mainObstacles.Values.ToArray()[random[i]].x;
            int y = (int)mainObstacles.Values.ToArray()[random[i]].y;
            int a; int b;
            if (i == 0) { a = Random.Range(0, 28 - x); b = Random.Range(0, 28 - y); }
            else if (i == 1) { a = Random.Range(28, 56 - x); b = Random.Range(0, 28 - y); }
            else if (i == 2) { a = Random.Range(0, 28 - x); b = Random.Range(28, 56 - y); }
            else { a = Random.Range(28, 56 - x); b = Random.Range(28, 56 - y); }

            mainObstacles[mainObstacles.Keys.ToArray()[random[i]]] = new Vector2((a + x * 0.5f) * 0.25f - 7, (b + y * 0.5f) * 0.25f - 7); 

            for (int n = a; n < a + x; n++)
            {
                for (int m = b; m < b + y; m++)
                {
                    stageRange[n, m] = true;
                }
            }
        }

        return mainObstacles;
    }
}
