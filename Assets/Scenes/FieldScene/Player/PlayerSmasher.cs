using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmasher : MonoBehaviour
{
    public void MakeGroggy(List<Collider> others, SmashAct smash)
    {
        others.ForEach(x => x.GetComponent<IGrogable>().Grog(smash));
    }
}
