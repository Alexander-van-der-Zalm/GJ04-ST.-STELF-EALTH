using UnityEngine;
using System.Collections;

public class BT_Inverter : BT_Decorator 
{
    public BT_Inverter() : base()
    {
        Description.Type = NodeDescription.BT_NodeType.Decorator;
        Description.Name = "BT_Inverter";
        Description.Description = "Pushes a value to the queue from the agents blackboard";
    }

    
    protected override Status update(AI_Agent agent,int id)
    {
        return exit(agent,invert(agent.Tree.GetFirstChild(id).Tick(agent)));
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
