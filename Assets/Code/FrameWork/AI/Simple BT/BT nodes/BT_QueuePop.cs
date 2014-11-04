using UnityEngine;
using Framework.Collections;

public class BT_QueuePop : BT_QueueBase 
{
    const string Value = "PopedObjLocation";

    #region Constructor

    public BT_QueuePop()
    {
        description();
    }

    private void description()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "QueuePop";
        Description.Description = "Pop one of the queue, into the parameterdestination of choice";
    }

    public override void SetNodeParameters(BT_TreeNode node)
    {
        this[Queue, node] = new AI_AgentBBAccessParameter();
        this[Value, node] = new AI_AgentBBAccessParameter();
    }

    #endregion

    #region Get Set

    public static BT_TreeNode GetTreeNode(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter PopParam)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_QueuePop());
        return SetParameters(node, QueueParam, PopParam);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter PopParam)
    {
        node.CheckAndSetClass<BT_QueuePop>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[Value, node] = PopParam;
        return node;
    }

    #endregion

    //public void SetParameters(BT_TreeNode node, string queueBBParameter, AI_Agent.BlackBoard queueBBAccessType, string poppedObjectSaveBBParameter, AI_Agent.BlackBoard objectBBAccessType)
    //{
    //    SetParameters(node, new AI_AgentBBAccessParameter(queueBBParameter, queueBBAccessType), new AI_AgentBBAccessParameter(poppedObjectSaveBBParameter, objectBBAccessType));
    //}

   


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
        SetAgentObject(Par(Value), agent, (object)q.Get());

        return Status.Succes;
    }
}
