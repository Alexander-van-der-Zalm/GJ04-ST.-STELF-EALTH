using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class BT_Selector : BT_Composite
{
    #region Constructor

    public BT_Selector(params BT_Behavior[] children)
    {
        Constructor(children.ToList());
    }

    public BT_Selector(List<BT_Behavior> children)
    {
        Constructor(children);
    }
    
    private void Constructor(List<BT_Behavior> children)
    {
        Description.Type = NodeDescription.BT_NodeType.Selector;

        Children = children;
    }

    #endregion

    #region Update

    protected override Status update(AI_Agent agent)
    {
        base.update(agent);

        for(int i = 0; i < Children.Count; i++)
        {
            Status s = Children[i].Tick(agent);

            // Continue on failed
            // Return succes, invalid and running
            if (s != Status.Failed)
                return exit(agent,s);
        }

        // Return failed if all the nodes are hit
        return exit(agent,Status.Failed);
    }

    #endregion
}
