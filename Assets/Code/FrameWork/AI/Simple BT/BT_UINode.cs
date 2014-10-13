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
    public BT_NodeMemory Node;

    //public BT_Behavior Parent;
    //public List<BT_Behavior> Children;

    public BT_Behavior.NodeDescription.BT_NodeType Type;

    //public List<AI_AgentBlackBoardAccessParameter> Test;

    private BT_BehaviorTree Tree;
    private AI_Blackboard param;
    private RectTransform rtr;

    #endregion

    #region Constructor

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
        Position = node.Position;
        rtr.position = Position;

        Node = node.Node;
        //Parent = node.Parent;
        //Children = node.Children;
        Tree = node.Tree;
        param.objectPool = node.Node.Description.NodeBlackBoard.objectPool;
        Type = node.Type;
    }
}
