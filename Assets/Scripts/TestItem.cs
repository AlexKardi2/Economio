using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ScriptableObject
{
    public int number;
    
    // Start is called before the first frame update
    void Start()
    {
        TestItem second = new() { number = 2 };
        TestItem third = new() { number = 3 };
        Debug.Log(second == third);
        Debug.Log(second.number + " "+third.number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    class ChildItem:ScriptableObject
    {
        internal int number { get; set; }

    }
}
