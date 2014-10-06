using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BT_Sequencer : BT_Composite
{
    #region Constructor

    public BT_Sequencer(AI_Agent agent, params BT_Behavior[] children) : base(agent)
    {
        Constructor(children.ToList());
    }

    public BT_Sequencer(AI_Agent agent, List<BT_Behavior> children)
        : base(agent)
    {
        Constructor(children);
    }
    
    private void Constructor(List<BT_Behavior> children)
    {
        Description.Type = NodeDescription.BT_NodeType.Sequencer;

        Children = children;
    }

    #endregion 

    protected override Status update()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            Status s = Children[i].Tick();

            // Continue on succes
            // Return failed, invalid and running
            if (s != Status.Succes)
                return s;
        }

        // Return succes if all the nodes are hit
        return Status.Succes;
    }
}
