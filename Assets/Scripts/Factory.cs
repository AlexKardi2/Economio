using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    static float gameSpeed = Status.s.gameSpeed;
    static int storageMultipler = Status.s.storageMultipler;

    [SerializeField] private Market market;

    public Item factory;
    float factoryNumber=1;

    public int wallet { get; private set;}
    Receipt receipt;
    Dictionary<Item, int> resources;
    Dictionary<Item, int> production;
    Dictionary<Item, Market.MarketOrder> sellMarketOrders;
    Item fuel;

    private bool productionStarted=false;
    float productionTime; //in sec


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (productionStarted) {

            productionTime -= Time.deltaTime * factoryNumber * gameSpeed;

            if (productionTime <= 0)
                foreach (KeyValuePair<Item, int> product in production)
                    production[product.Key] += receipt.production[product.Key];

            productionStarted = false;
        }

        if (!productionStarted) {
            bool startProduction = true;

            foreach (KeyValuePair<Item, int> resource in resources)
            {
                if ((resource.Value < receipt.resources[resource.Key]) || ((receipt.production[resource.Key]*storageMultipler*(int)factoryNumber-production[resource.Key])<receipt.production[resource.Key])) //ADD and CHANGE
                    startProduction = false;
            }
            if (startProduction)
        
                foreach (KeyValuePair<Item, int> resourse in resources)
                {
                    resources[resourse.Key] -= receipt.resources[resourse.Key];
                    productionTime += receipt.productionTime;
                    productionStarted = true;
                }
        }
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
            return true;
        }
    }
}
