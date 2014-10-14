using UnityEngine;
using System.Collections;

/// <summary>
/// Copies an object from the blackboard or directly passed on an object on the blackboard
/// </summary>
public class BT_CopyBBParameter : BT_Action 
{
    private const string Override = "OverrideLocation";
    private const string ObjParam = "CopyParameter";
    private const string Obj = "ObjectsToCopy";
    private const string IsObject = "UseObjectToCopy";

    #region Constructor

    public BT_CopyBBParameter()
    {
        description();
    }

    public override void SetNodeParameters(BT_TreeNode node)
    {
        this[Override, node] = new AI_AgentBBAccessParameter();
        this[ObjParam, node] = new AI_AgentBBAccessParameter();
        this[Obj, node] = null;
        this[IsObject, node] = false;
    }

    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "CopyBBParameter";
        Description.Description = "Copies the values from slot2 to slot1 and then succeeds";
    }

    #endregion

    #region Get

    public static BT_TreeNode GetTreeNode(AI_AgentBBAccessParameter accesparam1, object setObject)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_CopyBBParameter());
        return SetParameters(node, accesparam1, setObject);
    }

    public static BT_TreeNode GetTreeNode(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
    {
        return GetTreeNode(new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject);
    }


    public static BT_TreeNode GetTreeNode(AI_AgentBBAccessParameter accesparam1, AI_AgentBBAccessParameter accesparam2)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_CopyBBParameter());
        return SetParameters(node, accesparam1, accesparam2);
    }

    public static BT_TreeNode GetTreeNode(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        return GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    #endregion

    #region Set

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter accesparam1, object setObject)
    {
        node.CheckAndSetClass<BT_CopyBBParameter>();
        node.Behavior[Override, node] = accesparam1;
        node.Behavior[Obj, node] = setObject;
        node.Behavior[IsObject, node] = true;
        return node;
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
    {
        return SetParameters(node, new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject);
    }
    
    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter accesparam1, AI_AgentBBAccessParameter accesparam2)
    {
        node.CheckAndSetClass<BT_CopyBBParameter>();
        node.Behavior[Override, node] = accesparam1;
        node.Behavior[ObjParam, node] = accesparam2;
        node.Behavior[IsObject, node] = false;
        return node;
    }
    public static BT_TreeNode SetParameters(BT_TreeNode node, string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        return SetParameters(node, new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    #endregion

    protected override Status update(AI_Agent agent, int id)
    {
        // Get the object from slot 2
        object objectToCopy;
        if ((bool)this[IsObject])
            objectToCopy = this[Obj];
        else
            objectToCopy = GetAgentObject(Par(ObjParam), agent);

        // Set it on slot1
        SetAgentObject(Par(Override), agent, objectToCopy);

        return Status.Succes;
    }
}
