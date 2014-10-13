using UnityEngine;
using System.Collections;

public class BT_Inverter : BT_Decorator 
{
    public BT_Inverter(BT_Behavior child) : base(child) { }
    
    protected override Status update(AI_Agent agent,int id)
    {
        return invert(agent[id,0].Tick(agent,id));
    }

    private Status invert(Status status)
    {
        switch(status)
        {
            case Status.Failed:
                return Status.Succes;
            case Status.Succes:
                return Status.Failed;
            case Status.Running:
                return Status.Running;
            default:
                return Status.Invalid;
        }
    }


}
