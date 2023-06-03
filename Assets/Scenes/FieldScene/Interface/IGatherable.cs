using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGatherable
{
    public ItemParam Gathered(out float weight);

    public void Disappear();
}
