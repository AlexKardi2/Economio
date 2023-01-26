using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LimitedCompany : MonoBehaviour
{
    [SerializeField] protected Market market;

    public Item factory;
    protected float factoryNumber = 0f;

    public int wallet { get; protected set; } = 1000;

    public Recipe recipe { get; protected set; }

    internal abstract void ConfirmBuy(Item item, int number, int transaction, bool AddFactory);

    protected void DecreaseWallet (int transaction)
    {
        wallet -= transaction;
        if (wallet < 0)
        {
            print("ERROR!!! Omg. not enough money (( " + wallet + " on wallet");
            wallet = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
