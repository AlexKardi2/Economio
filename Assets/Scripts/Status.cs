using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    static public Status s { get; internal set; } //Singletone link

    //Balansing variables
    public float gameSpeed = 1f;
    public int storageMultipler = 5;
    public float requiredProfitability = 0.25f;
 
    //Variables for ingame change may be

    //Helpful Links
    //Market market=GameObject.Find("Market").GetComponent<Market>();

    void Awake()
    {
        if ((s!=this)&&(s!=null))
        {
            Destroy(s);
        } else
        {
            s = this;
        }

        Item.LoadItems();
        Recipe.LoadReceipts();
    }
    void Start()
    {
        /*
        TestItem second = new() { number = 2 };
        TestItem third = new() { number = 3 };
        Debug.Log(second == third);
        Debug.Log(second.number + " " + third.number);
        
        Market market = GameObject.Find("Market").GetComponent<Market>();
        Market newMarket = new();
        Factory newSecondMarket = new();
        Factory factiry = GameObject.Find("Factory").GetComponent<Factory>();
        print(market == newMarket);*/
    }


}
