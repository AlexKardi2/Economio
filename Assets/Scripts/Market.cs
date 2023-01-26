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

    public bool Buy(LimitedCompany buyer, MarketOrder sellOrder, int number = 1, bool addFactory = false)
    {
        if (number> sellOrder.number)
        {
            Debug.Log("Error!!! Someone is trying to buy more then there is in SellOrder");
            return false;
        }
        if (buyer.wallet<(sellOrder.price*number))
        {
            Debug.Log("Error!!! There is no enough money to buy");
            return false;
        }

        if (!sellOrder.owner.ConfirmSell(sellOrder.item, number, number * sellOrder.price))
            return false;

        buyer.ConfirmBuy(sellOrder.item, number, number * sellOrder.price, addFactory);

        sellOrder.number -= number;
        StorePriceHistory(sellOrder.item, sellOrder.price);
        return true;
    }

    public void Buy(LimitedCompany buyer, Item item, int number, bool addFactory=false)
    {
        List<MarketOrder> sellOrders = GetOrderListofItem(item);

        while ((number > 0) && (sellOrders.Count > 0))
        {
            int bestPriceIndex = 0;
            for (int i = 0; i < sellOrders.Count; i++)
                if (sellOrders[i].price < sellOrders[bestPriceIndex].price)
                    bestPriceIndex = i;
            int buyValue = Mathf.Max(number, sellOrders[bestPriceIndex].number);
            if (Buy(buyer, sellOrders[bestPriceIndex], buyValue, addFactory))
            {
                number -= buyValue;
                sellOrders.RemoveAt(bestPriceIndex);
            }
        }
    }

    private void DeleteOrder (MarketOrder order)
    {
        while (orderList.Contains(order))
            orderList.Remove(order);
    }

    private void AddOrder(MarketOrder order)
    {
        if (!orderList.Contains(order))
            orderList.Add(order);
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

    public int? GetLastSellPrice (Item item)
    {
        int? minimalPrice=GetBestPrice(item);

        if (priceHistory.ContainsKey(item))
            if (minimalPrice == null || priceHistory[item] < minimalPrice) minimalPrice = priceHistory[item];
        return minimalPrice;
    }

    private List<MarketOrder> GetOrderListofItem (Item item)
    {
        List<MarketOrder> itemOrderlist = new();
        foreach (MarketOrder current in itemOrderlist)
        {
            if (current.item == item)
                itemOrderlist.Add(current);
        }
        return itemOrderlist;
    }

    public int? GetBestPrice (Item item)
    {
        List<MarketOrder> sellOrders = GetOrderListofItem(item);
        if (sellOrders.Count == 0)
            return null;

        int bestPriceIndex = 0;
        for (int i = 0; i < sellOrders.Count; i++)
            if (sellOrders[i].price < sellOrders[bestPriceIndex].price)
                bestPriceIndex = i;

        return sellOrders[bestPriceIndex].price;
    }
  
    public class MarketOrder
    {
        static readonly Market market = GameObject.Find("Market").GetComponent<Market>();
        private readonly bool sellOrder;
        public readonly Factory owner;
        public readonly Item item;
        private int _number;
        public int number { get=> _number; set 
            {
                if (value >= 0)
                {
                    if (_number > 0 && value == 0)
                    {
                        market.DeleteOrder(this);
                    }
                    else if (_number == 0 && value > 0)
                        market.AddOrder(this);
                    _number = value;
                }
                else
                    Debug.Log("ERROR!!! Someone was trying to decrease amount in MarketOrder less then 0");
            }
        }
        private int _price;
        public int price { get => _price; set
            {
                _price = (value > 0) ? value : 0;
            }
        }

        public MarketOrder(Factory owner, Item item, int number=0, int price=0)
        {
            this.owner = owner; 
            this.item = item; 
            this.number = number; 
            this.price = price;
            sellOrder = true;
        }
        public MarketOrder(Factory owner, string itemName, int number=0, int price=0) : this(owner, Item.GetItem(itemName), number, price) { }


    }
}
