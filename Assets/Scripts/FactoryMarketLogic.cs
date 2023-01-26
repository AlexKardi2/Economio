using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMarketLogic : MonoBehaviour
{
    Factory plant;
    float requiredProfitability;
    Dictionary<Item, Market.MarketOrder> sellMarketOrders = new();
    Market market;

    private float? GetCostPrice(bool availableToBuy = true)
    {

        float price = 0;
        

        foreach (KeyValuePair<Item, int> resourse in plant.recipe.resources)
        {
            int? addPrice;
            addPrice = market.GetBestPrice(resourse.Key);
            if (addPrice == null)
            {
                if (availableToBuy == true)
                {
                    return null;
                }
                addPrice = market.GetLastSellPrice(resourse.Key) ?? 0;
            }
            price += (int)addPrice;
        }

        //TODO add factory amortisation
        //addPrice = market.GetBestPrice(plant.factory) / plant.factory. ;

        price += (market.GetBestPrice(plant.fuel) ?? 0) * plant.fuel.energyInside / plant.recipe.energyRequired;

        return price;

    }

    // Start is called before the first frame update
    void Start()
    {
        plant = gameObject.GetComponent<Factory>();
        requiredProfitability = Status.s.requiredProfitability;
        market = GameObject.Find("Market").GetComponent<Market>();

        if (plant.recipe==null)
        {
            plant.SetRecipeManually();
            if (plant.recipe == null)
            {
                print("ERROR!!! There is no recipe in factory " + name);
                return;
            }
        }

        foreach (KeyValuePair<Item, int> product in plant.recipe.production)
        {
            sellMarketOrders.Add(product.Key, new(plant, product.Key));
        }
    }

    private float Fillness(Item item, bool inProduction = true) => (plant.FreeSpace(item, inProduction) / plant.Capacity(item, inProduction));
    


    // Update is called once per frame
    void Update()
    {
        
    }

    internal void ThinkAboutPrice(Item item)
    {
        throw new NotImplementedException();
    }
}
