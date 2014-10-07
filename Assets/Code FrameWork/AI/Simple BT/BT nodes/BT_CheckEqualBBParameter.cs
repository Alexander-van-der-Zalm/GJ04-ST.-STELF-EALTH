using UnityEngine;
using System.Collections;

public class BT_CheckEqualBBParameter : BT_BBParameter 
{
    public BT_CheckEqualBBParameter(string bbParameter, AI_Agent.BlackBoard accesparam1, object equalObject)
        : base(bbParameter, accesparam1, equalObject)
    {
        Description.Type = NodeDescription.BT_NodeType.Condition;
    }

    public BT_CheckEqualBBParameter(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : base(bbParameter1, param1, bbParameter2,param2)
    {
        Description.Type = NodeDescription.BT_NodeType.Condition;
    }

    protected override Status update(AI_Agent agent)
    {
        object obj1 = GetObject(agent,parameter1,parameter1bb,false);
        object obj2 = GetObject(agent, parameter2, parameter2bb, param2IsObject);

        // If equals, then succes, else failed
        return obj1.Equals(obj2) ? Status.Succes : Status.Failed;
    }

   
}
