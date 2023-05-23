using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrogable
{
    //public bool Groggy { get; }

    public void Grog(Smasher smash, float time);
}
