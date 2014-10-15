using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable,ExecuteInEditMode]
public class BT_UINode : MonoBehaviour
{
    public BT_UINodeInfo NodeInfo;

    private AI_Blackboard param;
    private RectTransform rtr;
   
    void Start()
    {
        param = this.GetOrAddComponent<AI_Blackboard>();
        param.Name = "Public Node parameters";
    }

    void Update()
    {
        if (rtr == null)
            rtr = GetComponent<RectTransform>();

        NodeInfo.Position = rtr.position;
    }

    internal void ChangeNode(BT_UINodeInfo node)
    {
        NodeInfo = node;
        NodeInfo.UINode = this;
        if(NodeInfo.Position != null)
            rtr.position = NodeInfo.Position;
        param.objectPool = node.TreeNode.NodeSpecificParameters.objectPool;
    }
}
