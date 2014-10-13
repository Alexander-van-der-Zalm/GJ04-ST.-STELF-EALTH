using UnityEngine;
using System.Collections;

public class BT_AlwayFail : BT_Decorator 
{
    public BT_AlwayFail() : base() 
    {
        Description.Name = "BT_AlwayFail";
        Description.Description = "Always returns failed";
    }

    protected override BT_Behavior.Status update(AI_Agent agent, int id)
    {
        agent.Tree.GetFirstChild(id).Tick(agent);
        return exit(agent, Status.Failed);
    }
}
