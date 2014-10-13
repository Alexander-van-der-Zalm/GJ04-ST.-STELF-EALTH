using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Status = BT_Behavior.Status;

[System.Serializable]
public class BT_TreeNode
{
    public BT_TreeNode Parent;
    public List<BT_TreeNode> Children;
    public int ID;
    public BT_Behavior Node;

    #region Properties

    public bool HasChildren { get { return Children.Count > 0; } }
    public bool IsRoot { get { return Parent == null; } }

    #endregion

    public BT_TreeNode(BT_Behavior behavior)
    {
        Node = behavior;
    }

    public BT_TreeNode(int id, BT_Behavior behaviour, params BT_TreeNode[] childrenMem)
    {
        Node = behaviour;
        ID = id;
        Children = Children.ToList();
    }

    public Status Tick(AI_Agent agent)
    {
        return Node.Tick(agent, ID);
    }
}