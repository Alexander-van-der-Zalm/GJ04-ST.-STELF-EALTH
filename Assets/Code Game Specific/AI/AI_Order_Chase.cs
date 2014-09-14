using UnityEngine;
using System.Collections;

public class AI_Order_Chase : AI_Order
{
    public override void StartOrder(AI_UtilityBox box)
    {
        base.StartOrder(box);

        //StartCoroutine(ChaseCoroutine())
    }
	
}
