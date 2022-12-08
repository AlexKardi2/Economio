using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static List<Item> itemList = new();

    public string itemName;

    bool liquid=false;
    bool factory = false;

    public static Item GetItem(string name)
    {
        foreach (Item item in itemList)
        {
            if (item.itemName == "name")
                return item;
        }
        return null;
    }

    private Item(string name, bool isFactory = false, bool isLiquid=false)
    {
        itemName = name;
        factory = isFactory;
        liquid = isLiquid;
    }

    internal static void LoadItems()
    {
        itemList.Add(new Item("copper-plate"));
        itemList.Add(new Item("iron-plate"));
        itemList.Add(new Item("iron-ore"));
        itemList.Add(new Item("copper-ore"));
        itemList.Add(new Item("iron-gear-wheel"));
        itemList.Add(new Item("iron-stick"));
        itemList.Add(new Item("assembling-machine-0", true));
        itemList.Add(new Item("coal"));
        itemList.Add(new Item("wood"));
        itemList.Add(new Item("stone"));

    }
}
