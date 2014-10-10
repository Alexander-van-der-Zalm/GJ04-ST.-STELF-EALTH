using UnityEngine;
using Framework.Collections;

public class BT_QueuePush : BT_BB2Parameters  
{
    public BT_QueuePush(string queueBBParameter, AI_Agent.BlackBoard accesparam1, object setObject)
        : base(queueBBParameter, accesparam1, setObject)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }

    public BT_QueuePush(string queueBBParameter, AI_Agent.BlackBoard queueBBAccessType, string objectBBParameter, AI_Agent.BlackBoard objectBBAccessType)
        : base(queueBBParameter, queueBBAccessType, objectBBParameter, objectBBAccessType)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }

    protected override Status update(AI_Agent agent)
    {
        // Get queue from the blackboard
        object obj = GetObject1(agent);
        // Check if it is actually the right type of queue
        if (obj.GetType().GetGenericTypeDefinition() != typeof(Queue<>))
        {
            Debug.Log("BT_QueuePush has invalid object behind paramters, needs: " + typeof(Queue<>));
            return Status.Invalid;
        }
        // Cast
        IQueue q = (IQueue)obj;
        // Save popped item to blackboard
        q.Add(GetObject2(agent));

        return Status.Succes;
    }
}
