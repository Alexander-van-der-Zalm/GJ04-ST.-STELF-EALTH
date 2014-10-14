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

    #endregion

    #region Custom Parameter setters

    public void SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter accesparam1, object setObject)
    {
        node.Behavior = this;
        this[Override, node] = accesparam1;
        this[Obj, node] = setObject;
        this[IsObject, node] = true;
    }

    public void SetParameters(BT_TreeNode node, string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
    {
        SetParameters(node, new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject);
    }
    public void SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter accesparam1, AI_AgentBBAccessParameter accesparam2)
    {
        node.Behavior = this;
        this[Override, node] = accesparam1;
        this[ObjParam, node] = accesparam2;
        this[IsObject, node] = false;
    }
    public void SetParameters(BT_TreeNode node, string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        SetParameters(node, new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    #endregion


    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "CopyBBParameter";
        Description.Description = "Copies the values from slot2 to slot1 and then succeeds";
    }

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
