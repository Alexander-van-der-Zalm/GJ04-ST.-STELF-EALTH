using UnityEngine;
using Framework.Collections;

public class BT_QueuePop : BT_QueueBase 
{
    const string Value = "PopedObjLocation";

    public BT_QueuePop()
    {
        description();
        this[Queue] = new AI_AgentBBAccessParameter();
        this[Value] = new AI_AgentBBAccessParameter();
    }

    public BT_QueuePop(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter PopParam)
    {
        description();
        this[Queue] = QueueParam;
        this[Value] = PopParam;
    }

    public BT_QueuePop(string queueBBParameter, AI_Agent.BlackBoard queueBBAccessType, string poppedObjectSaveBBParameter, AI_Agent.BlackBoard objectBBAccessType)
        : this(new AI_AgentBBAccessParameter(queueBBParameter, queueBBAccessType), new AI_AgentBBAccessParameter(poppedObjectSaveBBParameter, objectBBAccessType))
    {
    }

    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "QueuePop";
        Description.Description = "Pop one of the queue, into the parameterdestination of choice";
    }


    protected override Status update(AI_Agent agent, int id)
    {
        // Get queue from the blackboard
        object obj = GetAgentObject(Par(Queue),agent);
        
        // Check if it is actually the right type of queue
        if (!HasIQueue(obj)) 
            return Status.Invalid;

        // Cast
        IQueue q = (IQueue)obj;
        // Save popped item to blackboard
        SetAgentObject(Par(Value), agent, q.Get());

        return Status.Succes;
    }
}
