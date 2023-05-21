using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdrenalineController : MonoBehaviour
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

    private List<IAdrenalineUI> adrenalineUIs;

    // Start is called before the first frame update
    void Start()
    {
        adrenalineUIs = GameObject.Find("Canvas")?.GetComponentsInChildren<IAdrenalineUI>().ToList();
        adrenalineUIs.ForEach(x => x.UpdateAdrenalineUI(Adrenaline));
        adrenalineUIs.ForEach(x => x.UpdateAdrenalineTankUI(AdrenalineTank));
    }

    // Update is called once per frame
    void Update()
    {
        Adrenaline -= Time.deltaTime * 0.1f;
        adrenalineUIs.ForEach(x => x.UpdateAdrenalineUI(Adrenaline));
    }

    public void IncreaseAdrenaline(float adre)
    {
        Adrenaline += adre;
        if (Adrenaline >= 1f)
        {
            Adrenaline = 0.5f;
            AdrenalineTank += 1;
            adrenalineUIs.ForEach(x => x.UpdateAdrenalineTankUI(AdrenalineTank));
        }
        adrenalineUIs.ForEach(x => x.UpdateAdrenalineUI(Adrenaline));
    }

    public void BurstAdrenalineTank(int tank)
    {
        if (AdrenalineTank > 0) AdrenalineTank -= tank;
        adrenalineUIs.ForEach(x => x.UpdateAdrenalineTankUI(AdrenalineTank));
    }
}
