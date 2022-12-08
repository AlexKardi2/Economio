using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    static public Status s { get; internal set; } //Singletone link

    //Balansing variables
    public float gameSpeed = 1f;
    public int storageMultipler = 5;
 
    //Variables for ingame change may be

    //Helpful Links
    //Market market= ADD

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
        Receipt.LoadReceipts();
    }


}
