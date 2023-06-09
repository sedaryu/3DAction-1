using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemParam", menuName = "Custom/ItemParam")]
public class ItemParam : ScriptableObject
{
    public enum ItemType
    { 
        Point,
        Life,
        Random
    }

    public ItemType Type => type;
    [SerializeField] private ItemType type;

    public string Name => name;
    [SerializeField] private new string name;

    public float Unique => unique;
    [SerializeField] private float unique;

    public float Weight => weight;
    [SerializeField] private float weight;

    public float Time => time;
    [SerializeField] private float time;

    public float Rank => rank;
    [SerializeField] private float rank;

    public int Drop => GetDrop();
    [SerializeField] private int dropMin;
    [SerializeField] private int dropMax;

    private int GetDrop()
    { 
        return Random.Range(dropMin, dropMax + 1);
    }

    public string Text => GetText();

    private string GetText()
    {
        if (Type == ItemType.Point) return "Point Get!!!";
        else if (Type == ItemType.Life) return "Life Up!!!";
        else if (Type == ItemType.Random) return "% Up Rarity!!!";
        else return "";
    }
}
