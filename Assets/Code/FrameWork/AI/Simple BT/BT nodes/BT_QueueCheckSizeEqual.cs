using UnityEngine;
using Framework.Collections;

public class BT_QueueCheckSizeEqual : BT_BB2Parameters
{
    public BT_QueueCheckSizeEqual(string queueBBParameter, AI_Agent.BlackBoard accesparam1, object compareObject)
        : base(queueBBParameter, accesparam1, compareObject)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }

    public BT_QueueCheckSizeEqual(string queueBBParameter, AI_Agent.BlackBoard queueBBAccessType, string compareObjectBBParameter, AI_Agent.BlackBoard objectBBAccessType)
        : base(queueBBParameter, queueBBAccessType, compareObjectBBParameter, objectBBAccessType)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }

    protected override Status update(AI_Agent agent)
    {
        // Get queue from the blackboard
        object obj = GetObject1(agent);
        object obj2 = GetObject2(agent);
        // Check if it is actually the right type of queue
        if (obj.GetType().GetGenericTypeDefinition() != typeof(Queue<>))
        {
            Debug.Log("BT_QueuePush has invalid object behind paramters, needs: " + typeof(Queue<>));
            return Status.Invalid;
        }
        // Cast
        IQueue q = (IQueue)obj;

        int size = (int)obj2;
        
        return q.Count == size ? Status.Succes : Status.Failed;
    }
}
