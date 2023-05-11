using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Act
{
    private Dictionary<string, Action> triggers_void = new Dictionary<string, Action>();
    private Dictionary<string, Action<string>> triggers_string = new Dictionary<string, Action<string>>();
    private Dictionary<string, Action<int>> triggers_int = new Dictionary<string, Action<int>>();

    public void AddTrigger(string key, Action value)
    {
        if (value == null) return;
        triggers_void.Add(key, value);
    }
    public void AddTrigger(string key, Action<string> value)
    {
        if (value == null) return;
        triggers_string.Add(key, value);
    }
    public void AddTrigger(string key, Action<int> value)
    {
        if (value == null) return;
        triggers_int.Add(key, value);
    }

    protected void OnTrigger(string key)
    {
        if (!triggers_void.ContainsKey(key)) { Debug.Log($"{key} is not addition in Dictionary"); return; }
        triggers_void[key].Invoke();
    }
    protected void OnTrigger(string key, string param)
    {
        if (!triggers_string.ContainsKey(key)) { Debug.Log($"{key} is not addition in Dictionary"); return; }
        triggers_string[key].Invoke(param);
    }
    protected void OnTrigger(string key, int param)
    {
        if (!triggers_int.ContainsKey(key))  { Debug.Log($"{key} is not addition in Dictionary"); return; }
        triggers_int[key].Invoke(param);
    }
}
