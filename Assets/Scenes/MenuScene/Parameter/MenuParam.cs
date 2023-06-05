using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class MenuParam
{
    public float Parameter(string key)
    {
        if (!parameters.ContainsKey(key)) throw new NullReferenceException();
        return parameters[key];
    }
    private Dictionary<string, float> parameters = new Dictionary<string, float>();

    public MenuParam(List<string> keys, List<float> values) 
    {
        parameters = new Dictionary<string, float>();
        for (int i = 0; i < keys.Count; i++)
        {
            parameters.Add(keys[i], values[i]);
        }
    }

    public void SetParameter(string key, float value)
    {
        if (!parameters.ContainsKey(key)) throw new NullReferenceException();
        if (value < 0) parameters[key] = 0;
        else if (parameters[key + "Max"] < value) parameters[key] = parameters[key + "Max"];
        else parameters[key] = value;
    }

    public void ChangeParameter(string key, float value)
    {
        if (!parameters.ContainsKey(key)) throw new NullReferenceException();
        if (parameters[key] + value < 0) parameters[key] = 0;
        else if (parameters[key + "Max"] < parameters[key] + value) parameters[key] = parameters[key + "Max"];
        else parameters[key] += value;
    }

    public Dictionary<string, float> SaveParameter()
    { 
        return parameters;
    }
}
