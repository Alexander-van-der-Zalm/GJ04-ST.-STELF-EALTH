using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System;

public class BT_QueuePop : BT_BB2Parameters 
{
    //public BT_QueuePop(string bbParameter, AI_Agent.BlackBoard accesparam1)
    //    : base(bbParameter, accesparam1)
    //{
    //    Description.Type = NodeDescription.BT_NodeType.Action;
    //}

    public BT_QueuePop(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : base(bbParameter1, param1, bbParameter2,param2)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }

    protected override Status update(AI_Agent agent)
    {
        // Get the object from queue
        object queue = GetObject2(agent);

        MethodInfo method = typeof(BT_QueuePop).GetMethod("GenericDequeue");
        MethodInfo generic = method.MakeGenericMethod(queue.GetType());
        Debug.Log(generic.Name + " - " + queue.GetType().ToString());
        object obj2 = generic.Invoke(this, null);
        
        // Set dequeued object to the second slot
        SetObject2(agent, obj2);

        return Status.Succes;
    }

    //public object GetFromGenericQueue(object obj)
    //{
    //    MethodInfo method = typeof(BT_QueuePop).GetMethod("GenericDequeue");
    //    MethodInfo generic = method.MakeGenericMethod(obj.GetType().GetGenericArguments()[0]);
        
    //    return generic.Invoke(this, new object[] { obj });
    //}

    //public object GenericDequeue<T>(Queue<T> q)
    //{
    //    return (object) q.Dequeue();
    //}
}
