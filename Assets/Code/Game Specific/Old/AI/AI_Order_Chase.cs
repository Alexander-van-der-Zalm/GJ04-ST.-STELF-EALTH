using UnityEngine;
using System.Collections;

public class AI_Order_Chase : AI_Order
{
    public override void StartOrder(AI_UtilityBox box)
    {
        base.StartOrder(box);
        
        StartCoroutine(ChaseCoroutine());
    }

    private IEnumerator ChaseCoroutine()
    {
        while(box.Los.LOS)
        {
            box.Controller.Start(box.Los.target.transform.position);
            yield return null;
        }
        IsFinished = true;
    }
	
}
