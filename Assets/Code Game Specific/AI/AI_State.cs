using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_State : MonoBehaviour 
{
    // Orders to loop through
    protected List<AI_Order> Orders =  new List<AI_Order>();
    protected AI_UtilityBox box;

    private bool paused = true;
    private int index = 0;

    protected bool OrderLoops;

    // Set to true if you want to let lower priority tasks take over
    public bool IsFinished = false;

    public void Create(AI_UtilityBox boks)
    {
        box = boks;
    }

    #region Pause & Resume

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

    #endregion

    // Main coroutine
    private IEnumerator StateCR()
    {
        // Start the first order
        Orders[index].StartOrder(box);

        while(!paused)
        {
            // Keep running till paused
            // Raise the index if the order is finished
            if(Orders[index].IsFinished)
            {
                Debug.Log("Order " + index + " is finished");
                //index = (index+1)%Orders.Count;
                index++;
                if(index<Orders.Count)
                    Orders[index].StartOrder(box);
                else
                {
                    IsFinished = true;
                    yield break;
                }
               
            }
            yield return null;
        }
    }

    // Reset the index of the orders
    public virtual void Reset()
    {
        index = 0;
    }
	

    public void ChangeOrderIndex(int newIndex)
    {

    }
}
