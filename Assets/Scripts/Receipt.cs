using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt : MonoBehaviour
{
    private static List<Receipt> receiptList = new();

    private const float STANDARTProdTime = 10;

    public string receiptName;
    public Dictionary <Item, int> resources=new();
    public Dictionary <Item, int> production=new();
    
    public float productionTime; //in sec

    public static Receipt GetReceipt(string name)
    {
        foreach (Receipt rec in receiptList)
        {
            if (rec.receiptName == "name")
                return rec;
        }
        return null;
    }

    private Receipt(string name, float time = STANDARTProdTime, int number = 1)
    {
        receiptName = name;
        productionTime = time;
        Item prodItem = Item.GetItem(name);
        if (prodItem != null)
            production.Add(prodItem, number);
    }

    internal static void LoadReceipts()
    {
        Receipt rec;

        rec = new Receipt("iron-stick", STANDARTProdTime, 2);
        rec.resources.Add(Item.GetItem("iron-plate"), 1);
        receiptList.Add(rec);

        rec = new Receipt("iron-plate", STANDARTProdTime, 1);
        rec.resources.Add(Item.GetItem("iron-ore"), 1);
        receiptList.Add(rec);

    }
}
