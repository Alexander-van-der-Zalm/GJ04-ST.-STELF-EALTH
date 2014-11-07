using UnityEngine;
using System.Collections;

public class BT_CheckEqualBBParameter : BT_Condition
{
    private const string P1 = "Parameter1ToCompare";
    private const string P2 = "Parameter2ToCompare";
    private const string Obj = "ObjectToCompare";
    private const string IsObject = "UseObjectToCompare";

    #region Constructors

    public BT_CheckEqualBBParameter()
    {
        description();
    }

    public override void SetNodeParameters(BT_TreeNode node)
    {
        this[P1, node] = new AI_AgentParameter();
        this[P2, node] = new AI_AgentParameter();
        this[Obj, node] = null;
        this[IsObject, node] = false;
    }
    
    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "CheckEqualBBParameter";
        Description.Description = "Succeeds if objects are equal and fails otherwise";
    }

    #endregion

    #region Get Set

    public static BT_TreeNode GetTreeNode(AI_AgentParameter accesparam1, object equalObject)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_CheckEqualBBParameter());
        return SetParameters(node, accesparam1, equalObject);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentParameter accesparam1, object equalObject)
    {
        node.CheckAndSetClass<BT_CheckEqualBBParameter>();
        node.Behavior[P1, node] = accesparam1;
        node.Behavior[Obj, node] = equalObject;
        node.Behavior[IsObject, node] = true;
        return node;
    }

    public static BT_TreeNode GetTreeNode(AI_AgentParameter accesparam1, AI_AgentParameter accesparam2)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_CheckEqualBBParameter());
        return SetParameters(node, accesparam1, accesparam2);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentParameter accesparam1, AI_AgentParameter accesparam2)
    {
        node.CheckAndSetClass<BT_CheckEqualBBParameter>();
        node.Behavior[P1,node] = accesparam1;
        node.Behavior[P2, node] = accesparam2;
        node.Behavior[IsObject, node] = false;
        return node;
    }

    //public BT_CheckEqualBBParameter(AI_AgentBBAccessParameter accesparam1, object equalObject)
    //{
    //    description();
    //    this[P1] = accesparam1;
    //    this[Obj] = equalObject;
    //    this[IsObject] = true;
    //}

    //public BT_CheckEqualBBParameter(string bbParameter, AI_Agent.BlackBoard accesparam1, object equalObject)
    //    : this(new AI_AgentBBAccessParameter(bbParameter, accesparam1), equalObject)
    //{
    //}
    //public BT_CheckEqualBBParameter(AI_AgentBBAccessParameter accesparam1, AI_AgentBBAccessParameter accesparam2)
    //{
    //    description();
    //    this[P1] = accesparam1;
    //    this[P2] = accesparam2;
    //    this[IsObject] = false;
    //}
    //public BT_CheckEqualBBParameter(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    //    : this(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2))
    //{
    //}


    #endregion


    protected override Status update()
    {      
        // Get the objects
        object obj1 = GetAgentObject(Par(P1),Agent);
        object obj2;
        if ((bool)this[IsObject])
            obj2 = this[Obj];
        else
            obj2 = GetAgentObject(Par(P2), Agent);

        // If equals, then succes, else failed
        return obj1.Equals(obj2) ? Status.Succes : Status.Failed;
    }

   
}
