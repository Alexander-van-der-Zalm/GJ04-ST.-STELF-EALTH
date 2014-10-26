using UnityEngine;
using System.Collections;
using Status = BT_Behavior.Status;
using fc = Framework.Collections;
public class BT_TreeConstructor 
{
    #region BT Component Syntactic Sugar

    public static BT_TreeNode qPush(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        return BT_QueuePush.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    public static BT_TreeNode qPush(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    {
        return BT_QueuePush.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), obj);
    }

    public static BT_TreeNode qPop(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        return BT_QueuePop.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    public static BT_TreeNode qSize(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        return BT_QueueCheckSizeEqual.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    public static BT_TreeNode qSize(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    {
        return BT_QueueCheckSizeEqual.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), obj);
    }

    public static BT_TreeNode copy(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        //return new BT_TreeNode(new BT_CopyBBParameter(bbParameter1, param1, bbParameter2, param2));
        //return BT_CopyBBParameter.GetTreeNode();
        return BT_CopyBBParameter.GetTreeNode(bbParameter1, param1, bbParameter2, param2);
    }

    public static BT_TreeNode copy(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    {
        return BT_CopyBBParameter.GetTreeNode(bbParameter1, param1, obj);
    }

    //public static BT_TreeNode copy(AI_AgentBBAccessParameter param1, object obj)
    //{
    //    return BT_CopyBBParameter.GetTreeNode(param1, obj);
    //}

    public static BT_TreeNode eqBB(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        return BT_CheckEqualBBParameter.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    public static BT_TreeNode eqBB(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    {
        return BT_CheckEqualBBParameter.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), obj);
    }

    public static BT_TreeNode fail(BT_TreeNode child)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_AlwayFail());
        node.AddChildren(child);
        return node;
    }

    public static BT_TreeNode inv(BT_TreeNode child)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_Inverter());
        node.AddChildren(child);
        return node;
    }

    public static BT_TreeNode sel(params BT_TreeNode[] children)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_Selector());
        node.AddChildren(children);
        return node;
    }

    public static BT_TreeNode seq(params BT_TreeNode[] children)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_Sequencer());
        node.AddChildren(children);
        return node;
    }

    #endregion

    #region Delegator

    public static BT_TreeNode UDel(BT_BehaviorDelegator.UpdateDelegate del, string name)
    {
        BT_BehaviorDelegator b = new BT_BehaviorDelegator(BT_Behavior.NodeDescription.BT_NodeType.Action, del);
        b.Description.Name = name;
        BT_TreeNode node = BT_TreeNode.CreateNode(b);
        return node;
    }

    public static BT_Behavior.Status failUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Failed;
    }

    public static BT_Behavior.Status succesUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Succes;
    }

    public static BT_Behavior.Status runningUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Running;
    }

    public static BT_Behavior.Status pauseUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        int Depth = (int)agent["Depth"];
        Debug.Break();
        return Status.Succes;
    }

    #endregion
}
