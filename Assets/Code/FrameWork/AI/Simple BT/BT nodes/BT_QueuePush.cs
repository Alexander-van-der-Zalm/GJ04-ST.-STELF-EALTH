using UnityEngine;
using Framework.Collections;

public class BT_QueuePush : BT_QueueBase  
{
    private const string PushObjPar = "ObjectToPushParameter";
    private const string Obj = "ObjectToPush";
    private const string IsObject = "ToPushIsObject";

    public BT_QueuePush(AI_AgentBBAccessParameter QueueParam, object pushObj)
    {
        description();
        this[Queue] = QueueParam;
        this[Obj] = pushObj;
        this[IsObject] = true;
    }

    public BT_QueuePush(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
        : this(new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject)
    {
    }
    public BT_QueuePush(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter PushParam)
    {
        description();
        this[Queue] = QueueParam;
        this[PushObjPar] = PushParam;
        this[IsObject] = false;
    }
    public BT_QueuePush(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : this(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2))
    {
    }

    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "BT_QueuePush";
        Description.Description = "Pushes a value to the queue from the agents blackboard";
    }

    protected override Status update(AI_Agent agent, int id)
    {
        // Get queue from the blackboard
        object obj = GetAgentObject(Par(Queue), agent);

        // Check if it is actually the right type of queue
        if (!HasIQueue(obj))
            return Status.Invalid;
       
        // Cast
        IQueue q = (IQueue)obj;
        
        // Get the object
        object objectToPush;
        if ((bool)this[IsObject])
            objectToPush = this[Obj];
        else
            objectToPush = GetAgentObject(Par(PushObjPar), agent);

        // Push item to the blackboard
        q.Add(objectToPush);

        return Status.Succes;
    }
}
