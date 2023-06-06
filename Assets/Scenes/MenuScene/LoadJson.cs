using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public EquipmentGunParam LoadEquipmentGunParam()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/StreamingAssets/Json/Menu/EquipmentGunParam.json");
        datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<EquipmentGunParam>(datastr);
    }

    public void SaveEquipmentGunParam(EquipmentGunParam equipGunParam)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(equipGunParam);

        writer = new StreamWriter(Application.dataPath + "/StreamingAssets/Json/Menu/EquipmentGunParam.json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
}

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

[System.Serializable]
public class EquipmentGunParam
{
    public List<string> guns;
    public int equipNumber = 0;
}
