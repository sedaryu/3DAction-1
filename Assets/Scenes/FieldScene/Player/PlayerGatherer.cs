using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerGatherer : MonoBehaviour
{
    private List<ItemParam> items = new List<ItemParam>();

    public float Gather(Collider other)
    {
        if (!other.TryGetComponent<IGatherable>(out IGatherable item)) return 0;

        items.Add(item.Gathered(out float weight));
        return weight;
    }

    public bool Release(Collider other)
    {
        if (!other.TryGetComponent<ItemCollector>(out ItemCollector collector)) return false;

        collector.CollectItems(items);
        items.Clear();
        return true;
    }

    public string ListUpItems()
    { 
        List<string> itemNames = items.Select(x => x.Name).Distinct().ToList();
        string itemList = "";
        foreach (string name in itemNames)
        { itemList += $"{name}: x{items.Where(x => x.Name == name).Count()}\n"; }
        return itemList;
    }
}
