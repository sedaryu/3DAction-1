using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStater : MonoBehaviour
{
    //èÛë‘
    public Dictionary<string, bool> State { get => state; }

    private Dictionary<string, bool> state = new Dictionary<string, bool>()
    { { "Movable", true }, { "Shootable", true }, { "Damageable", true }, { "Smashable", false } };

    public void TransferState(string key, bool param)
    {
        state[key] = param;
    }

    public IEnumerator WaitForStatusTransition(string key, float time)
    {
        state[key] = false;
        yield return new WaitForSeconds(time);
        state[key] = true;
    }
}
