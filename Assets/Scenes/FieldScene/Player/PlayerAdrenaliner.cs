using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAdrenaliner : MonoBehaviour
{
    public float Adrenaline
    {
        get => adrenaline;
        private set
        {
            if (value <= 0) adrenaline = 0;
            else adrenaline = value;
        }
    }
    private float adrenaline = 0;

    public int AdrenalineTank
    {
        get => adrenalineTank;
        private set
        {
            if (value >= 3) adrenalineTank = 3;
            else adrenalineTank = value;
        }
    }
    private int adrenalineTank = 0;

    public void DecreaseAdrenaline()
    {
        Adrenaline -= Time.deltaTime * 0.075f;
    }

    public void IncreaseAdrenaline()
    {
        Adrenaline += 0.2f;
        if (Adrenaline >= 1f)
        {
            Adrenaline = 0.5f;
            AdrenalineTank += 1;
        }
    }

    public void BurstAdrenalineTank(int tank)
    {
        if (AdrenalineTank > 0) AdrenalineTank -= tank;
    }
}
