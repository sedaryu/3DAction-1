using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJson
{
    public MenuParam LoadMenuParam()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/StreamingAssets/Json/Menu/MenuParam.json");
        datastr = reader.ReadToEnd();
        reader.Close();
        MenuParamJson json = JsonUtility.FromJson<MenuParamJson>(datastr);
        return new MenuParam(json.keys, json.values);
    }

    public void SaveMenuParam(MenuParam menuParam)
    {
        StreamWriter writer;

        MenuParamJson json = new MenuParamJson(menuParam);
        string jsonstr = JsonUtility.ToJson(json);

        writer = new StreamWriter(Application.dataPath + "/StreamingAssets/Json/Menu/MenuParam.json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
}
