using UnityEngine;
using Framework.Collections;

public class BT_QueuePush : BT_Action  
{
    private const string PushObjPar = "ObjectToPushParameter";
    private const string Obj = "ObjectToPush";
    private const string IsObject = "UseObjectToPush";
    protected const string Queue = "QueueParameter";

    #region Constructor

    public BT_QueuePush()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "QueuePush";
        Description.Description = "Pushes a value to the queue from the agents blackboard";
    }

    public override void SetNodeParameters(BT_TreeNode node)
    {
        this[Queue, node] = new AI_AgentParameter();
        this[PushObjPar, node] = new AI_AgentParameter();
        this[Obj, node] = null;
        this[IsObject, node] = false;
    }

    #endregion

    #region Get Set

    public static BT_TreeNode GetTreeNode(AI_AgentParameter QueueParam, object pushObj)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_QueuePush());
        return SetParameters(node, QueueParam, pushObj);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentParameter QueueParam, object toPushObject)
    {
        node.CheckAndSetClass<BT_QueuePush>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[Obj, node] = toPushObject;
        node.Behavior[IsObject, node] = true;
        return node;
    }

    public static BT_TreeNode GetTreeNode(AI_AgentParameter QueueParam, AI_AgentParameter PushParam)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_QueuePush());
        return SetParameters(node, QueueParam, PushParam);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentParameter QueueParam, AI_AgentParameter PushParam)
    {
        node.CheckAndSetClass<BT_QueuePush>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[PushObjPar, node] = PushParam;
        node.Behavior[IsObject, node] = false;
        return node;
    }

    #endregion
    
    protected override Status update()
    {
        // Get queue from the blackboard
        object obj = GetAgentObject(Par(Queue), Agent);

        //if(obj == null)
        //{
        //    obj = new Queue<
        //}

        // Check if it is actually the right type of queue
        if (!BT_QueueHelper.HasIQueue(obj))
        {
            Debug.LogError("BT_QueuePush: No queue exists in the blackboard");
            return Status.Invalid;
        }
       
        // Cast
        IQueue q = (IQueue)obj;
        
        // Get the object
        object objectToPush;
        if ((bool)this[IsObject])
            objectToPush = this[Obj];
        else
            objectToPush = GetAgentObject(Par(PushObjPar), Agent);

        // Push item to the blackboard
        q.Add(objectToPush);

        return Status.Succes;
    }
}
