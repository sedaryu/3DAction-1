using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameResulter : MonoBehaviour
{
    public List<string> collectItems;

    private Text itemsText;

    // Start is called before the first frame update
    void Start()
    {
        itemsText = GameObject.Find("Canvas").transform.Find("ItemsText").GetComponent<Text>();
        ListUpItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ListUpItems()
    {
        List<string> itemTypes = collectItems.Distinct().ToList();
        List<string> itemList = new List<string>();
        foreach (string type in itemTypes)
        { itemList.Add($"{type}: x{collectItems.Where(x => x == type).Count()}\n"); }

        foreach (string item in itemList)
        {
            //yield return new WaitForSeconds(2);
            itemsText.text += item;
        }
    }
}
