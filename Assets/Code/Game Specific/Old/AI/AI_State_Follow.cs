using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AI_State_Follow : AI_State 
{
    public void Start()
    {
        NewOrderList();
        // Think about re-entering LOS while in this state
    }
	
    private void NewOrderList()
    {
        Orders = new List<AI_Order>();
        Orders.Add(GetComponentInChildren<AI_Order_Chase>());
        //Orders.Add(GetComponentInChildren<AI_Order>());
        
        //Orders.Add(new )
    }

    public override void Resume()
    {
        base.Resume();
        Reset();
    }
}
