using UnityEngine;
using System.Collections;

public class BT_AlwayFail : BT_Decorator 
{
    public BT_AlwayFail(BT_Behavior child) : base(child) { }

    protected override Status update(AI_Agent agent)
    {
        return Status.Failed;
    }
}
