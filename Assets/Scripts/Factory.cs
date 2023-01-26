using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : LimitedCompany
{
    static float gameSpeed;
    static int storageMultipler;

    FactoryMarketLogic logic;

    [SerializeField] private string temporaryRecipeName;
   


    private Dictionary<Item, int> resources=new();
    private Dictionary<Item, int> production=new();
    public Item fuel { get; private set; }
    private float fuelNumber = 0f;

    private bool productionStarted=false;
    private float energyRequiredToProduce; //in kJ


    // Start is called before the first frame update
    void Start()
    {
        gameSpeed = Status.s.gameSpeed;
        storageMultipler = Status.s.storageMultipler;
        if (factoryNumber < 1)
            factoryNumber = 1f;
        logic = gameObject.GetComponent<FactoryMarketLogic>();

        if (recipe == null)
            SetRecipeManually();

    }

    internal override void ConfirmBuy(Item item, int number, int transaction, bool addFactory)
    {
        DecreaseWallet(transaction);
        
        if (!addFactory)
            resources[item] += number;
        else if (item == factory)
            factoryNumber += number;
        else
            print("ERROR!! Something wrong with adding 1 more factory");
    }



    // Update is called once per frame
    void Update()
    {
        if (factory == null) return;

        //TODO add factory amortisation (excluding last factory)

        if (productionStarted)
        {
            float energyToSpend = Time.deltaTime * factoryNumber * gameSpeed * factory.productionPower;
            if (fuelNumber * fuel.energyInside * 1000 >= energyToSpend)
            {
                fuelNumber -= energyToSpend / (fuel.energyInside * 1000);
                energyRequiredToProduce -= energyToSpend;
            }

            if (energyRequiredToProduce <= 0)
            {
                foreach (KeyValuePair<Item, int> product in production)
                    production[product.Key] += recipe.production[product.Key];
                productionStarted = false;
            }
        }

        if (!productionStarted) 
        {
            bool startProduction = true;

            foreach (KeyValuePair<Item, int> resource in resources)
            {
                if ((resource.Value < recipe.resources[resource.Key]) || (FreeSpace(resource.Key)>=recipe.production[resource.Key]))
                    startProduction = false;
            }

            if (startProduction)
                foreach (KeyValuePair<Item, int> resourse in resources)
                {
                    resources[resourse.Key] -= recipe.resources[resourse.Key];
                    energyRequiredToProduce += recipe.energyRequired;
                    productionStarted = true;
                }
        }
    }

    public void InstallRecipe(Recipe installingRecipe)
    {
        recipe = installingRecipe;
        factory = recipe.factory;

        print("Installing recipe " + recipe.recipeName);

        foreach (KeyValuePair<Item, int> product in recipe.production)
            production.Add(product.Key, 0);
        
        foreach (KeyValuePair<Item, int> resource in recipe.resources)
            resources.Add(resource.Key, 0);

        fuel = factory.fuelForFactory;
    }

    public bool ConfirmSell (Item item, int number, int transaction) 
    {
        if (production[item]<number)
        {
            print($"Some factory HAS NOT enough {item.itemName}. Has just {production[item]}, but at least {number} in sell order!!!");
            return false;
        } else
        {
            production[item] -= number;
            wallet += transaction;
            logic.ThinkAboutPrice(item);
            return true;
        }
    }

    internal int Capacity (Item item, bool inProduction=true)
    {
        int result = 0;
        if (inProduction)
            recipe.production.TryGetValue(item, out result);
        else
            recipe.resources.TryGetValue(item, out result);
        return result * storageMultipler * (int)factoryNumber;
    }

    internal int FreeSpace(Item item, bool inProduction = true)
    {
        int result = inProduction ? (Capacity(item) - production[item]) : (Capacity(item, false) - resources[item]);
        return (result < 0) ? 0 : result;
    }

    public void SetRecipeManually ()
    {
        Recipe recipeToInstall = Recipe.GetRecipe(temporaryRecipeName);
        if (recipeToInstall == null)
        {
            print("ERROR. Recipe is not installed. And not installed manualy");
            return;
        } else
        {
            InstallRecipe(recipeToInstall);
        }
    }
}
