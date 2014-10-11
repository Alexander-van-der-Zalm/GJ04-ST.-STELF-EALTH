using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BT_UINode : MonoBehaviour 
{
    public BT_Behavior.NodeDescription.BT_NodeType Type;
    public BT_Behavior Node;
    public int X, Y;

    public BT_BehaviorTree Tree;
    public BT_Behavior Parent;
    public List<BT_Behavior> Children;
    public Vector3 Position;

    public void SetParent(BT_UINode node)
    {
        Type type = node.Node.GetType();
        if(isValidParent(node))
            Parent = node.Node;
    }

    private bool isValidParent(BT_UINode node)
    {
        Type type = node.Node.GetType();
        
        if(type.IsSubclassOf(typeof(BT_Composite))||type.IsSubclassOf(typeof(BT_Decorator)))
            return true;

        return false;
    }



    internal void ChangeNode(BT_UINode node)
    {
        throw new NotImplementedException();
    }
}
