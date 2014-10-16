using UnityEngine;
using System.Collections;

[System.Serializable]
public class BT_UINodeInfo  
{
    public Vector3 Position;
    public BT_UINode UINode;

    public BT_BehaviorTree Tree { get; private set; }
    public BT_UINodeInfo Parent { get; private set; }
    public BT_TreeNode TreeNode { get; private set; }
    public int Depth { get; private set; }
    public int Index { get; private set; }
    public int ParentIndex { get { return Parent.Index; } }
    public int ChildrenCount { get { return TreeNode == null ? 0 : TreeNode.Children.Count; } }

    public BT_UINodeInfo(int depth, int rank, BT_TreeNode node, BT_UINodeInfo parent, BT_BehaviorTree tree)
    {
        Depth = depth;
        Index = rank;
        TreeNode = node;
        Parent = parent;
        Tree = tree;
    }

    public void SetBehavior(BT_BBParameters behavior)
    {
        if (TreeNode == null)
            TreeNode = new BT_TreeNode(behavior);
        else
            TreeNode.Behavior = behavior;
    }
}
