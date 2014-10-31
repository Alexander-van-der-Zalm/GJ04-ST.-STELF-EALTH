using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class BT_Selector : BT_Composite
{
    #region Constructor

    public BT_Selector()
    {
        Description.Type = NodeDescription.BT_NodeType.Composite;
        Description.Name = "Selector";
        Description.Description = "Keeps going if children are failing, early exits if: succes, invalid ör running";
    }

    #endregion

    #region Update

    protected override Status update(AI_Agent agent, int id)
    {
        base.update(agent,id);

        List<BT_TreeNode> nodes = agent.Tree.GetChildren(id);

        for (int i = 0; i < nodes.Count; i++)
        {
            Status s = nodes[i].Tick(agent);

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
