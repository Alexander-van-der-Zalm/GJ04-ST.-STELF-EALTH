﻿using UnityEngine;
using Framework.Collections;

public class BT_QueueCheckSizeEqual : BT_QueueBase
{
    private const string SizeObjPar = "SizeBBParameter";
    private const string Obj = "SizeObject";
    private const string IsObject = "UseObjectToCompare";

    public BT_QueueCheckSizeEqual()
    {
        description();
        this[Queue] = new AI_AgentBBAccessParameter();
        this[SizeObjPar] = new AI_AgentBBAccessParameter();
        this[Obj] = null;
        this[IsObject] = false;
    }

    public BT_QueueCheckSizeEqual(AI_AgentBBAccessParameter QueueParam, object sizeObj)
    {
        description();
        this[Queue] = QueueParam;
        this[Obj] = sizeObj;
        this[IsObject] = true;
    }

    public BT_QueueCheckSizeEqual(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
        : this(new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject)
    {
    }
    public BT_QueueCheckSizeEqual(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter sizeObj)
    {
        description();
        this[Queue] = QueueParam;
        this[SizeObjPar] = sizeObj;
        this[IsObject] = false;
    }
    public BT_QueueCheckSizeEqual(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : this(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2))
    {
    }

    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "QueueCheckSizeEqual";
        Description.Description = "Check if the queue size is equal to the object (use int/a number)";
    }

    protected override Status update(AI_Agent agent, int id)
    {
        // Get queue from the blackboard
        object obj = GetAgentObject(Par(Queue), agent);
        object obj2 = GetAgentObject(Par(SizeObjPar), agent);

        // Check if it is actually the right type of queue
        if (!HasIQueue(obj))
            return Status.Invalid;

        // Cast
        IQueue q = (IQueue)obj;

        int size = (int)obj2;
        
        return q.Count == size ? Status.Succes : Status.Failed;
    }
}
