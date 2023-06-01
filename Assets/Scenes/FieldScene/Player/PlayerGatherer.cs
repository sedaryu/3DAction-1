using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerGatherer : MonoBehaviour
{
    private List<string> items = new List<string>();

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
        List<string> itemTypes = items.Distinct().ToList();
        string itemList = "";
        foreach (string type in itemTypes)
        { itemList += $"{type}: x{items.Where(x => x == type).Count()}\n"; }
        return itemList;
    }
}
