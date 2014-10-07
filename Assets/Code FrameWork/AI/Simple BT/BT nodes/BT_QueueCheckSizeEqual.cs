using UnityEngine;
using System.Collections;

public class BT_QueueCheckSizeEqual : BT_BB2Parameters
{
    public BT_QueueCheckSizeEqual(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
        : base(bbParameter, accesparam1, setObject)
    {
        Description.Type = NodeDescription.BT_NodeType.Condition;
    }

    public BT_QueueCheckSizeEqual(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : base(bbParameter1, param1, bbParameter2,param2)
    {
        Description.Type = NodeDescription.BT_NodeType.Condition;
    }

    protected override Status update(AI_Agent agent)
    {
        Queue q = (Queue)GetObject1(agent);
        int size = (int)GetObject2(agent);

        return q.Count == size ? Status.Succes : Status.Failed;
    }
}
