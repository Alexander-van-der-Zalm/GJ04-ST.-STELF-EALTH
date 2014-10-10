using UnityEngine;
using System.Collections;

/// <summary>
/// Copies an object from the blackboard or directly passed on an object on the blackboard
/// </summary>
public class BT_CopyBBParameter : BT_BB2Parameters 
{
    public BT_CopyBBParameter(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
        : base(bbParameter, accesparam1, setObject)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }

    public BT_CopyBBParameter(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : base(bbParameter1, param1, bbParameter2,param2)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }

    protected override Status update(AI_Agent agent)
    {
        // Get the object from slot 2
        object objectToCopy = GetObject(agent, parameter2, parameter2bb, param2IsObject);

        // Set it on slot1
        SetObject(agent, parameter1, parameter1bb, objectToCopy);

        return Status.Succes;
    }
}
