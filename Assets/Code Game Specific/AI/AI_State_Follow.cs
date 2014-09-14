using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AI_State_Follow : AI_State 
{
    public void Start()
    {
        
    }
	
    public void Follow(Transform trans)
    {
        Reset();
        Resume();

    }

    private void NewOrderList(Transform trans)
    {
        Orders = new List<AI_Order>();
        AI_Order_Chase chase = new AI_Order_Chase();
        
        
        //Orders.Add(new )
    }
}
