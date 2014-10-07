using UnityEngine;
using System.Collections.Generic;
using System;

public class BT_QueueBase<T> : BT_BB2Parameters
{
    public BT_QueueBase(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : base(bbParameter1, param1, bbParameter2,param2)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        //SetObject1()
    }


    protected object Dequeue(AI_Agent agent, object queue)
    {
        Type queueType = typeof(Queue<>);

        if (queue.GetType().GetGenericTypeDefinition() != queueType)
        {
            Debug.Log("Error: Object is not a Generic Queue.");
            return null;
        }

        Type containedType = queue.GetType().GetGenericArguments()[0];

        var contstructedQueueType = queueType.MakeGenericType(containedType);
        var instance = Activator.CreateInstance(contstructedQueueType);
        return null;
    }

}

