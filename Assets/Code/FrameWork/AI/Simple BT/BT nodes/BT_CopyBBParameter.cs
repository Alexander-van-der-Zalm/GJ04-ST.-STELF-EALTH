using UnityEngine;
using System.Collections;

/// <summary>
/// Copies an object from the blackboard or directly passed on an object on the blackboard
/// </summary>
public class BT_CopyBBParameter : BT_Action 
{
    private const string P1 = "Parameter1";
    private const string P2 = "Parameter2";
    private const string Obj = "ObjectToCompare";
    private const string IsObject = "SecondParameterIsObject";
 
    public BT_CopyBBParameter(AI_AgentBBAccessParameter accesparam1, object setObject)
    {
        description();
        this[P1] = accesparam1;
        this[Obj] = setObject;
        this[IsObject] = true;
    }

    public BT_CopyBBParameter(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
        : this(new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject)
    {
    }
    public BT_CopyBBParameter(AI_AgentBBAccessParameter accesparam1, AI_AgentBBAccessParameter accesparam2)
    {
        description();
        this[P1] = accesparam1;
        this[P2] = accesparam2;
        this[IsObject] = false;
    }
    public BT_CopyBBParameter(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : this(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2))
    {
    }

    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "BT_CopyBBParameter";
        Description.Description = "Copies the values from slot2 to slot1 and then succeeds";
    }

    protected override Status update(AI_Agent agent)
    {
        // Get the object from slot 2
        object objectToCopy;
        if ((bool)this[IsObject])
            objectToCopy = this[Obj];
        else
            objectToCopy = GetAgentObject(Par(P2), agent);

        // Set it on slot1
        SetAgentObject(Par(P1), agent, objectToCopy);

        return Status.Succes;
    }
}
