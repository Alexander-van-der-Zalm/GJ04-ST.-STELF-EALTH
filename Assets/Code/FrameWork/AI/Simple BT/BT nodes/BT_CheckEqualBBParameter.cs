using UnityEngine;
using System.Collections;

public class BT_CheckEqualBBParameter : BT_Action 
{
    private const string P1 = "Parameter1ToCompare";
    private const string P2 = "Parameter2ToCompare";
    private const string Obj = "ObjectToCompare";
    private const string IsObject = "UseObjectToCompare";

    #region Constructors

    public BT_CheckEqualBBParameter()
    {
        description();
        this[P1] = new AI_AgentBBAccessParameter();
        this[P2] = new AI_AgentBBAccessParameter();
        this[Obj] = null;
        this[IsObject] = false;
    }

    public BT_CheckEqualBBParameter(AI_AgentBBAccessParameter accesparam1, object equalObject)
    {
        description();
        this[P1] = accesparam1;
        this[Obj] = equalObject;
        this[IsObject] = true;
    }

    public BT_CheckEqualBBParameter(string bbParameter, AI_Agent.BlackBoard accesparam1, object equalObject)
        : this(new AI_AgentBBAccessParameter(bbParameter, accesparam1), equalObject)
    {
    }
    public BT_CheckEqualBBParameter(AI_AgentBBAccessParameter accesparam1, AI_AgentBBAccessParameter accesparam2)
    {
        description();
        this[P1] = accesparam1;
        this[P2] = accesparam2;
        this[IsObject] = false;
    }
    public BT_CheckEqualBBParameter(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : this(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2))
    {
    }

    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "CheckEqualBBParameter";
        Description.Description = "Succeeds if objects are equal and fails otherwise";
    }

    #endregion


    protected override Status update(AI_Agent agent, int id)
    {
        // Get the objects
        object obj1 = GetAgentObject(Par(P1),agent);
        object obj2;
        if ((bool)this[IsObject])
            obj2 = this[Obj];
        else
            obj2 = GetAgentObject(Par(P2), agent);

        // If equals, then succes, else failed
        return obj1.Equals(obj2) ? Status.Succes : Status.Failed;
    }

   
}
