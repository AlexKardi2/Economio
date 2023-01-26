using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private static List<Item> itemList = new();
    public readonly string itemName;

    public readonly bool liquid = false;
    public readonly bool factory = false;
    public bool sciencePack { get; private set; } = false;
    public Item fuelForFactory { get; private set;} = null;
    public float productionPower { get; private set; } = Recipe.ONE_SEC_PRODUCTION_ENERGY;

    public readonly bool fuel = false;
    public int energyInside { get; private set; } = 0; //In MJ


    public static Item GetItem(string name)
    {
        foreach (Item item in itemList)
        {
            if (item.itemName == name)
            {
                return item;
            }
                
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
        //Load simple Items
        itemList.Add(new Item("copper-plate"));
        itemList.Add(new Item("iron-plate"));
        itemList.Add(new Item("iron-ore"));
        itemList.Add(new Item("copper-ore"));
        itemList.Add(new Item("iron-gear-wheel"));
        itemList.Add(new Item("iron-stick"));
        itemList.Add(new Item("coal")
            { energyInside = 4 });
        itemList.Add(new Item("wood"));
        itemList.Add(new Item("stone"));
        itemList.Add(new Item("stone-brick"));
        itemList.Add(new Item("engine-unit"));
        itemList.Add(new Item("stone-furnace", true));
        
        //Load factories Items 
        itemList.Add(new Item("burner-mining-drill", true));
        itemList[^1].fuelForFactory=Item.GetItem("coal");
        itemList.Add(new Item("assembling-machine-0", true));
        itemList[^1].productionPower *= 0.5f;
        itemList[^1].fuelForFactory = Item.GetItem("coal");

        //Load science packs
        itemList.Add(new Item("automation-science-pack") 
            { sciencePack = true });

    }
}
