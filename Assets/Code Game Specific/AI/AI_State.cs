using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_State : MonoBehaviour 
{
    // Orders to loop through
    protected List<AI_Order> Orders =  new List<AI_Order>();
    protected bool paused = true;
    protected int index = 0;
    protected AI_UtilityBox box;

    public void Create(AI_UtilityBox boks)
    {
        box = boks;
    }

    // Stops the main CoRoutine
    public virtual void Pause()
    {
        paused = true;
        StopAllCoroutines();
    }

    // Initiates the main CoRoutine
    public virtual void Resume()
    {
        paused = false;
        StartCoroutine(StateCR());
    }

    // Main coroutine
    private IEnumerator StateCR()
    {
        while(!paused)
        {
            // Keep running till paused
            // Raise the index if the order is finished
            if(Orders[index].IsFinished)
            {
                index = (index+1)%Orders.Count;
                Orders[index].StartOrder(box);
            }
            yield return null;
        }
    }

    // Reset the index of the orders
    public virtual void Reset()
    {
        index = 0;
    }
	
}
