﻿using UnityEngine;
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
        this[Queue, node] = new AI_AgentBBAccessParameter();
        this[PushObjPar, node] = new AI_AgentBBAccessParameter();
        this[Obj, node] = null;
        this[IsObject, node] = false;
    }

    #endregion

    #region Get Set

    public static BT_TreeNode GetTreeNode(AI_AgentBBAccessParameter QueueParam, object pushObj)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_QueuePush());
        return SetParameters(node, QueueParam, pushObj);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter QueueParam, object toPushObject)
    {
        node.CheckAndSetClass<BT_QueuePush>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[Obj, node] = toPushObject;
        node.Behavior[IsObject, node] = true;
        return node;
    }

    public static BT_TreeNode GetTreeNode(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter PushParam)
    {
        BT_TreeNode node = BT_TreeNode.CreateNode(new BT_QueuePush());
        return SetParameters(node, QueueParam, PushParam);
    }

    public static BT_TreeNode SetParameters(BT_TreeNode node, AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter PushParam)
    {
        node.CheckAndSetClass<BT_QueuePush>();
        node.Behavior[Queue, node] = QueueParam;
        node.Behavior[PushObjPar, node] = PushParam;
        node.Behavior[IsObject, node] = false;
        return node;
    }

    #endregion


    //public BT_QueuePush(AI_AgentBBAccessParameter QueueParam, object pushObj)
    //{
    //    description();
    //    this[Queue] = QueueParam;
    //    this[Obj] = pushObj;
    //    this[IsObject] = true;
    //}

    //public BT_QueuePush(string bbParameter, AI_Agent.BlackBoard accesparam1, object setObject)
    //    : this(new AI_AgentBBAccessParameter(bbParameter, accesparam1), setObject)
    //{
    //}
    //public BT_QueuePush(AI_AgentBBAccessParameter QueueParam, AI_AgentBBAccessParameter PushParam)
    //{
    //    description();
    //    this[Queue] = QueueParam;
    //    this[PushObjPar] = PushParam;
    //    this[IsObject] = false;
    //}
    //public BT_QueuePush(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    //    : this(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2))
    //{
    //}

    
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
