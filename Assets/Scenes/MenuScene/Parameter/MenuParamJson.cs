using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MenuParamJson
{
    public List<string> keys;
    public List<float> values;

    public MenuParamJson(MenuParam menuParam) 
    { 
        keys = menuParam.SaveParameter().Keys.ToList();
        values = menuParam.SaveParameter().Values.ToList();
    }
}
