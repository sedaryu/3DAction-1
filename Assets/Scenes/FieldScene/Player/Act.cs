using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Act
{
    protected Dictionary<string, Action<string>> triggers = new Dictionary<string, Action<string>>();

    public void AddTrigger(string key, Action<string> value)
    {
        if (value == null) return;
        triggers.Add(key, value);
    }

    protected void OnTrigger(string key, string param)
    {
        if (triggers[key] == null) return;
        triggers[key].Invoke(param);
    }
}
