using UnityEngine;
using System.Collections;

public class AI_Order : MonoBehaviour 
{
    public bool IsFinished;
    protected AI_UtilityBox box;

    public virtual void StartOrder(AI_UtilityBox box)
    {
        this.box = box;
        IsFinished = false;
    }
}
