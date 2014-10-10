using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BT_Sequencer : BT_Composite
{
    #region Constructor

    public BT_Sequencer(params BT_Behavior[] children) 
    {
        Constructor(children.ToList());
    }

    public BT_Sequencer(List<BT_Behavior> children)
    {
        Constructor(children);
    }
    
    private void Constructor(List<BT_Behavior> children)
    {
        Description.Type = NodeDescription.BT_NodeType.Sequencer;

        Children = children;
    }

    #endregion 

    protected override Status update(AI_Agent agent)
    {
        base.update(agent);

        for (int i = 0; i < Children.Count; i++)
        {
            Status s = Children[i].Tick(agent);

            // Continue on succes
            // Return failed, invalid and running
            if (s != Status.Succes)
                return exit(agent,s);
        }

        // Return succes if all the nodes are hit
        return exit(agent,Status.Succes);
    }
}
