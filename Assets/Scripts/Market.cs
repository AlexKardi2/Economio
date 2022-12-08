using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    private List<MarketOrder> orderList = new();
    private Dictionary<Item, int> priceHistory=new();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StorePriceHistory (Item item, int price)
    {
        if (!priceHistory.ContainsKey(item))
        {
            priceHistory.Add(item, price);
        } else
        {
            priceHistory[item] = price;
        }
    }

    public int GetLastPrice (Item item)
    {
        int minimalPrice=int.MaxValue;

        foreach (MarketOrder checkingOrder in GetOrderListofItem(item))
        {
            if (checkingOrder.price< minimalPrice) minimalPrice = checkingOrder.price;
        }
        if (priceHistory.ContainsKey(item))
            if (priceHistory[item] < minimalPrice) minimalPrice = priceHistory[item];
        return minimalPrice;
    }

    private List<MarketOrder> GetOrderListofItem (Item item)
    {
        List<MarketOrder> itemOrderlist = new();
        foreach (MarketOrder current in orderList)
        {
            if (current.item == item)
                itemOrderlist.Add(current);
        }
        if (itemOrderlist.Count == 0)
            return null;
        else
            return itemOrderlist;
    }

    //public void CreateSellOrder ADD
    
    public class MarketOrder
    {        
        private bool sellOrder;
        public Factory owner { get; private set; }
        public Item item { get; private set; }
        public int number { get;
            set 
            {
                if (number==0)
                {
                    number = value;
                    
                }
            } 
        }
        public int price { get; private set; }

        public MarketOrder(Factory owner, Item item, int price, int number)
        { 
            this.owner = owner; 
            this.item = item; 
            this.number = number; 
            this.price = price;
        }
        public MarketOrder(Factory owner, string itemName, int number, int price) : this(owner, Item.GetItem(itemName), number, price) { }


    }
}
