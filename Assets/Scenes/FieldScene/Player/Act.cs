using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Act
{
    private Dictionary<string, Action> triggers_void = new Dictionary<string, Action>();
    private Dictionary<string, Action<string>> triggers_string = new Dictionary<string, Action<string>>();
    private Dictionary<string, Action<bool>> triggers_bool = new Dictionary<string, Action<bool>>();
    private Dictionary<string, Action<float>> triggers_float = new Dictionary<string, Action<float>>();

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
    public void AddTrigger(string key, Action<bool> value)
    {
        if (value == null) return;
        triggers_bool.Add(key, value);
    }
    public void AddTrigger(string key, Action<float> value)
    {
        if (value == null) return;
        triggers_float.Add(key, value);
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
    protected void OnTrigger(string key, bool param)
    {
        if (!triggers_bool.ContainsKey(key)) { Debug.Log($"{key} is not addition in Dictionary"); return; }
        triggers_bool[key].Invoke(param);
    }
    protected void OnTrigger(string key, float param)
    {
        if (!triggers_float.ContainsKey(key)) { Debug.Log($"{key} is not addition in Dictionary"); return; }
        triggers_float[key].Invoke(param);
    }
}
