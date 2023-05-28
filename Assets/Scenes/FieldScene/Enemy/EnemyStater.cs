using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStater : MonoBehaviour
{
    //èÛë‘
    public Dictionary<string, bool> State { get => state; }

    private Dictionary<string, bool> state = new Dictionary<string, bool>()
    { { "Movable", true }, { "Attackable", true }, { "Grogable", false }, { "Smashable", false }, {"Destroyable", false} };

    public void TransferState(string key, bool param)
    { 
        state[key] = param;
    }

    public IEnumerator WaitForStateTransition(string key, float time)
    {
        state[key] = false;
        yield return new WaitForSeconds(time);
        state[key] = true;
    }

    public void TransferDestroyableState()
    {
        state["Movable"] = false;
        state["Attackable"] = false;
        state["Grogable"] = true;
        state["Smashable"] = true;
        state["Destroyable"] = true;
    }
}
