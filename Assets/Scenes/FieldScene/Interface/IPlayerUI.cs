using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerUI
{
    public void UpdateUI(string key, float value);

    public void UpdateUI(string key, string value);
}
