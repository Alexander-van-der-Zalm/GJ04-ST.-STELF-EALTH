using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BT_Sequencer : BT_Composite
{
    #region Constructor

    public BT_Sequencer()
    {
        SetDescription();
    }

    //public BT_Sequencer(params BT_Behavior[] children) 
    //{
    //    Constructor(children.ToList());
    //}

    //public BT_Sequencer(List<BT_Behavior> children)
    //{
    //    Constructor(children);
    //}
    
    //private void Constructor(List<BT_Behavior> children)
    //{
        
    //    SetDescription();

    //    Children = children;
    //}

    private void SetDescription()
    {
        Description.Type = NodeDescription.BT_NodeType.Composite;
        Description.Name = "Sequencer";
        Description.Description = "Keeps going if children are succesfull, early exits if: failed, invalid ör running";
    }

    #endregion 

    protected override Status update(AI_Agent agent,int id)
    {
        base.update(agent,id);

        List<BT_TreeNode> nodes = agent.Tree.GetChildren(id);

        for (int i = 0; i < nodes.Count; i++)
        {
            Status s = nodes[i].Tick(agent);

            // Continue on succes
            // Return failed, invalid and running
            if (s != Status.Succes)
                return exit(agent,s);
        }

        // Return succes if all the nodes are hit
        return exit(agent,Status.Succes);
    }
}
