using UnityEngine;
using System.Linq;
using Framework.Collections;

public class BT_QueueCheckSizeEqual : BT_Condition
{
    private const string SizeObjPar = "SizeBBParameter";
    private const string Obj = "SizeObject";
    private const string IsObject = "UseObjectToCompare";
    protected const string Queue = "QueueParameter";

    #region Constructor

    public BT_QueueCheckSizeEqual()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
        Description.Name = "QueueCheckSizeEqual";
        Description.Description = "Check if the queue size is equal to the object (use int/a number)";
    }

    public override void SetNodeParameters(BT_TreeNode node)
    {
        this[Queue, node] = new AI_AgentParameter();
        this[SizeObjPar, node] = new AI_AgentParameter();
        //this[Obj, node] = null;
        node.ParametersBB[Obj] = null;
        this[IsObject, node] = false;
    }

    #endregion

    #region Get and Set

    public static BT_TreeNode GetTreeNode(AI_AgentParameter QueueParam, object sizeObject)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_QueueCheckSizeEqual());
        return SetParameters(node, QueueParam, sizeObject);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentParameter QueueParam, object sizeObject)
    {
        node.CheckAndSetClass<BT_QueueCheckSizeEqual>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[Obj, node] = sizeObject;
        node.Behavior[IsObject, node] = true;
        return node;
    }

    public static BT_TreeNode GetTreeNode(AI_AgentParameter QueueParam, AI_AgentParameter sizeObjParam)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_QueueCheckSizeEqual());
        return SetParameters(node, QueueParam, sizeObjParam);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentParameter QueueParam, AI_AgentParameter sizeObj)
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

    

    protected override Status update()
    {
        // Get queue from the blackboard
        object obj = GetAgentObject(Par(Queue), Agent);
        
        // If there is no Queue return invalid
        if (obj == null || !BT_QueueHelper.HasIQueue(obj))
        {
            Debug.LogError("BT_QueueCheckSizeEqual: No queue exists in the blackboard");
            return Status.Invalid;
        }
            
        
        // Cast
        IQueue q = (IQueue)obj;

        object SizeObj;
        if ((bool)this[IsObject])
            SizeObj = this[Obj];
        else
            SizeObj = GetAgentObject(Par(SizeObjPar), Agent);
        // Check if it is actually the right type of queue
    
        int size = (int)SizeObj;
        
        return q.Count == size ? Status.Succes : Status.Failed;
    }
}
