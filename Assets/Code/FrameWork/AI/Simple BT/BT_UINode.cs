using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BT_UINode : MonoBehaviour
{
    #region Fields

    public Vector3 Position;
    //public List<BT_Behavior> Children;

    // TODO remove this
    public BT_Behavior.NodeDescription.BT_NodeType Type { get { return Node.Behavior.Description.Type; } }

    //public List<AI_AgentBlackBoardAccessParameter> Test;

    private BT_BehaviorTree tree;
    private AI_Blackboard param;
    private RectTransform rtr;

    public BT_UINode Parent { get; private set; }
    public BT_TreeNode Node { get; private set; }
    public int Depth { get; private set; }
    public int Index { get; private set; }
    public int ParentIndex { get { return Parent.Index; } }
    public int ChildrenCount { get { return Node.Children.Count; } }
    
    #endregion

    #region Constructor

    public BT_UINode(int depth, int rank, BT_TreeNode node, BT_UINode parent, BT_BehaviorTree tree)
    {
        Depth = depth;
        Index = rank;
        Node = node;
        Parent = parent;
        this.tree = tree;
    }

    void Start()
    {
        param = this.GetOrAddComponent<AI_Blackboard>();
        param.Name = "Public Node parameters";
    }

    #endregion

    //public BT_UINode CreateUINode()


    //public void SetParent(BT_UINode node)
    //{
    //    Type type = node.Node.GetType();
    //    if(isValidParent(node))
    //        Parent = node.Node;
    //}

    //private bool isValidParent(BT_UINode node)
    //{
    //    Type type = node.Node.GetType();
        
    //    if(type.IsSubclassOf(typeof(BT_Composite))||type.IsSubclassOf(typeof(BT_Decorator)))
    //        return true;

    //    return false;
    //}

    void Update()
    {
        if (rtr == null)
            rtr = GetComponent<RectTransform>();

        Position = rtr.position;
    }


    internal void ChangeNode(BT_UINode node)
    {
        // Redo THIS
        Debug.Log("Redo BT_UINode.ChangeNode");
        Position = node.Position;
        rtr.position = Position;

        Node = node.Node;
        //Parent = node.Parent;
        //Children = node.Children;
        tree = node.tree;
        param.objectPool = node.Node.NodeSpecificParameters.objectPool;
        //Type = node.Type;
    }

}
