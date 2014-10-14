using UnityEngine;
using Framework.Collections;

public class BT_QueueCheckSizeEqual : BT_QueueBase
{
    private const string SizeObjPar = "SizeBBParameter";
    private const string Obj = "SizeObject";
    private const string IsObject = "UseObjectToCompare";

    #region Constructor

    public BT_QueueCheckSizeEqual()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "QueueCheckSizeEqual";
        Description.Description = "Check if the queue size is equal to the object (use int/a number)";
    }

    public override void SetNodeParameters(BT_TreeNode node)
    {
        this[Queue, node] = new AI_AgentBBAccessParameter();
        this[SizeObjPar, node] = new AI_AgentBBAccessParameter();
        this[Obj, node] = null;
        this[IsObject, node] = false;
    }

    #endregion

    #region Get and Set

    public static BT_TreeNode GetTreeNode(AI_AgentBBAccessParameter QueueParam, object sizeObject)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_QueueCheckSizeEqual());
        return SetParameters(node, QueueParam, sizeObject);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter QueueParam, object sizeObject)
    {
        node.CheckAndSetClass<BT_QueueCheckSizeEqual>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[Obj, node] = sizeObject;
        node.Behavior[IsObject, node] = true;
        return node;
    }

    public static BT_TreeNode GetTreeNode(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter sizeObjParam)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_QueueCheckSizeEqual());
        return SetParameters(node, QueueParam, sizeObjParam);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter sizeObj)
    {
        node.CheckAndSetClass<BT_QueueCheckSizeEqual>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[SizeObjPar, node] = sizeObj;
        node.Behavior[IsObject, node] = false;
        return node;
    }

    #endregion

    //public BT_QueueCheckSizeEqual(AI_AgentBBAccessParameter QueueParam, object sizeObj)
    //{
    //    description();
    //    this[Queue] = QueueParam;
    //    this[Obj] = sizeObj;
    //    this[IsObject] = true;
    //}

    //public BT_QueueCheckSizeEqual(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
    //    : this(new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject)
    //{
    //}
    //public BT_QueueCheckSizeEqual(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter sizeObj)
    //{
    //    description();
    //    this[Queue] = QueueParam;
    //    this[SizeObjPar] = sizeObj;
    //    this[IsObject] = false;
    //}
    //public BT_QueueCheckSizeEqual(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    //    : this(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2))
    //{
    //}

    

    protected override Status update(AI_Agent agent, int id)
    {
        // Get queue from the blackboard
        object obj = GetAgentObject(Par(Queue), agent);
 
        object SizeObj;
        if ((bool)this[IsObject])
            SizeObj = this[Obj];
        else
            SizeObj = GetAgentObject(Par(SizeObjPar), agent);
        // Check if it is actually the right type of queue
        if (!HasIQueue(obj))
            return Status.Invalid;

        // Cast
        IQueue q = (IQueue)obj;

        int size = (int)SizeObj;
        
        return q.Count == size ? Status.Succes : Status.Failed;
    }
}
