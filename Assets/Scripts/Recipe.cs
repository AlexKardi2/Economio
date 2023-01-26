using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    private static List<Recipe> recipeList = new();

    public static readonly int ONE_SEC_PRODUCTION_ENERGY= 150; //in kJ looks like
    public static readonly int STANDART_ENERGY_MULTIPLER = 1; //In seconds within standart production time

    public string recipeName;
    public readonly Dictionary<Item, int> resources=new();
    public readonly Dictionary<Item, int> production=new();
    public float energyRequired; //In kJ
    

    public List<Recipe> prerequirements { get; private set; } = new();
    
    private Item _factory;

    public bool invented;
    private List<Item> sciensePacks;
    public int sciensePacksMultipler = 0;

    public Item factory
    {
        get
        { return _factory; }
        private set
        {
            if (value.factory == true)
            {
                _factory = value;
            }
        }
    }

    private void AddSciencePack(int packsMultipler, params string[] packs)
    {
        if (packs.Length == 0) return;
        if (sciensePacks == null)
            sciensePacks = new();

        sciensePacksMultipler = (packsMultipler > 0) ? packsMultipler : 1;
        foreach (string packName in packs)
        {
            Item pack = Item.GetItem(packName);
            if ((pack != null) && (!sciensePacks.Contains(pack)) && pack.sciencePack == true)
                sciensePacks.Add(pack);
        }
        if (sciensePacks.Count > 0)
            invented = false;
    }

    private void AddResource(string resourceName, int number)
    {
        Item resource = Item.GetItem(resourceName);
        if (resource!=null && !resources.ContainsKey(resource))
        {
            resources.Add(resource, number);
        } else
        {
            Debug.Log("ERROR! with adding resource " + resourceName);
        }

    }



    public static Recipe GetRecipe(string name)
    {
        foreach (Recipe rec in recipeList)
        {
            if (rec.recipeName == name)
                return rec;
        }
        return null;
    }

    private Recipe(string name, float energyRequiredMultipler = 1f, int number = 1)
    {
        recipeName = name;
        this.energyRequired = energyRequiredMultipler*ONE_SEC_PRODUCTION_ENERGY;
        factory = Item.GetItem("assembling-machine-0");
        Item prodItem = Item.GetItem(name);
        if (prodItem != null)
            production.Add(prodItem, number);
    }

    private Recipe(string name, int number) : this(name, STANDART_ENERGY_MULTIPLER, number) { }

    private Recipe(string name, int number, string resource, int resource_number=1) : this (name, STANDART_ENERGY_MULTIPLER, number)
    {
        resources.Add(Item.GetItem(resource), resource_number);
    }

internal static void LoadReceipts()
    {
        recipeList.Add(new Recipe("iron-stick", 2, "iron-plate"));
        recipeList.Add(new Recipe("iron-plate", 1, "iron-ore"));
        recipeList[^1].factory = Item.GetItem("stone-furnace");
        recipeList.Add(new Recipe("assembling-machine-0"));
        recipeList[^1].AddResource("iron-plate", 8);
        recipeList[^1].AddResource("stone-brick", 4);
        recipeList[^1].AddResource("engine-unit", 1);
        recipeList[^1].AddSciencePack(1, "automation-science-pack");
        recipeList.Add(new Recipe("engine-unit"));
        recipeList[^1].AddResource("iron-plate", 1);
        recipeList[^1].AddResource("iron-gear-wheel", 1);
        recipeList.Add(new Recipe("iron-gear-wheel", 1, "iron-plate",2));
        recipeList.Add(new Recipe("burner-mining-drill"));
        recipeList[^1].AddResource("iron-gear-wheel",3);
        recipeList[^1].AddResource("stone-furnace", 1);
        recipeList[^1].AddResource("iron-plate", 3);
        recipeList.Add(new Recipe("stone-furnace",1,"stone",5));
        recipeList.Add(new Recipe("stone-brick",3.2f, 1)); ;
        recipeList[^1].factory = Item.GetItem("stone-furnace");
        recipeList[^1].AddResource("stone", 2);
    }
}
