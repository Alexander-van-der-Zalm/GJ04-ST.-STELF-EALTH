using UnityEngine;
using Framework.Collections;

public class BT_QueuePop : BT_Action 
{
    string Queue = "Queue";
    string Value = "ReturnSetValueLocation";
    public BT_QueuePop(string queueBBParameter, AI_Agent.BlackBoard queueBBAccessType, string poppedObjectSaveBBParameter, AI_Agent.BlackBoard objectBBAccessType)
       // : //base(queueBBParameter, queueBBAccessType, poppedObjectSaveBBParameter, objectBBAccessType)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "BT_QueuePop";
        Description.Description = "Pop one of the queue, into the parameterdestination of choice";
        this[Queue] = new AI_AgentBBAccessParameter(queueBBParameter, queueBBAccessType);
        this[Value] = new AI_AgentBBAccessParameter(poppedObjectSaveBBParameter, objectBBAccessType);
    }

    public BT_QueuePop()
    {

    }

    protected override Status update(AI_Agent agent)
    {
        
        // Get queue from the blackboard
        object obj = GetAgentObject(Par(Queue),agent);
        // Check if it is actually the right type of queue
        if (obj.GetType().GetGenericTypeDefinition() != typeof(Queue<>))
        {
            Debug.Log("BT_QueuePush has invalid object behind paramters, needs: " + typeof(Queue<>));
            return Status.Invalid;
        }
        // Cast
        IQueue q = (IQueue)obj;
        // Save popped item to blackboard
        SetAgentObject(Par(Value), agent, q.Get());

        return Status.Succes;
    }
}
