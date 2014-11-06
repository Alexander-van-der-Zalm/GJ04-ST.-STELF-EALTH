using UnityEngine;
using System.Linq;
using System.Collections;

public class BT_Inverter : BT_Decorator 
{
    public BT_Inverter() : base()
    {
        Description.Name = "Inverter";
        Description.Description = "Fires off child and then returns the inverted status (failed if success etc.)";
    }

    
    protected override Status update()
    {
        //base.update(Agent, id);

        if (!Node.HasChildren)
            return Status.Invalid;

        return invert(Node.Children.First().Tick(Agent));//
        //exit(agent,invert(agent.Tree.GetFirstChild(id).Tick(agent)));
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
