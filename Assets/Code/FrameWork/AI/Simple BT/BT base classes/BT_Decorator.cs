using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BT_Decorator : BT_HasChild 
{
    //protected BT_Behavior child;

    #region Constructor

    public BT_Decorator()
    {
        setDescription();
    }


    private void setDescription()
    {
        Description.Type = NodeDescription.BT_NodeType.Decorator;
    }

    #endregion

    protected override Status update(AI_Agent agent, int id)
    {
        base.update(agent, id);

        List<BT_TreeNode> nodes = agent.Tree.GetChildren(id);

        // No child means that the decorater is invalid
        if(nodes.Count <= 0)
            return exit(agent, Status.Invalid);

        // See what the decorater has to say
        Status s = DecoraterMethod(agent, id);

        // Eject out if the decorator is not succesfull
        if (s != Status.Succes)
            return exit(agent, s);

        // Return the result of the child if the decorator has been succesfull
        return exit(agent,nodes[0].Tick(agent));
    }


    protected virtual Status DecoraterMethod(AI_Agent agent, int id)
    {
        return Status.Invalid;
    }
}
