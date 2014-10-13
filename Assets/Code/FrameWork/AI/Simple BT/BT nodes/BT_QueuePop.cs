using UnityEngine;
using Framework.Collections;

public class BT_QueuePop : BT_BB2Parameters 
{
    public BT_QueuePop(string queueBBParameter, AI_Agent.BlackBoard queueBBAccessType, string poppedObjectSaveBBParameter, AI_Agent.BlackBoard objectBBAccessType)
        : base(queueBBParameter, queueBBAccessType, poppedObjectSaveBBParameter, objectBBAccessType)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description["Blabla"] = new AI_AgentBBAccessParameter("Blabla", AI_Agent.BlackBoard.global);
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
        SetObject2(agent, q.Get());

        return Status.Succes;
    }
}
