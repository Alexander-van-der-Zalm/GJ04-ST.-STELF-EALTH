using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AI_State_Follow : AI_State 
{
    public void Start()
    {
        NewOrderList();
    }
	
    //public void Follow(Transform trans)
    //{
    //    Reset();
    //    Resume();
    //}

    private void NewOrderList()
    {
        Orders = new List<AI_Order>();
        Orders.Add(GetComponentInChildren<AI_Order_Chase>());
        //Orders.Add(GetComponentInChildren<AI_Order>());
        
        //Orders.Add(new )
    }
}
