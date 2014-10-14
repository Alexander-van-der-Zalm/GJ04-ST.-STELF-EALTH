using UnityEngine;
using System.Linq;
using Framework.Collections;
using System.Collections;
using System.Collections.Generic;
using Status = BT_Behavior.Status;
using fc = Framework.Collections;


using System;
using System.Reflection;

public class BT_BehaviorTree : MonoBehaviour
{
    #region Fields

    [Range(0.00000000001f,60)]
    public float UpdateFrequency = 10;

    private BT_TreeNode Root;
    private List<BT_UINode> UINodes;
    private Dictionary<int,BT_TreeNode> TreeNodes;

    private int TreeIteration = 0;

    public BT_TreeNode this[int id]
    {
        get { return TreeNodes[id]; }
    }

    public int Version { get { return TreeIteration; } }

    #endregion

    #region Init & Update

    //// Use this for initialization
    //void Start () 
    //{
    //    //TestTreeFunctionality();
    //    if(Tree != null)
    //        StartCoroutine(updateCR());
    //}

    // Update loop
    public IEnumerator updateCR(AI_Agent agent)
    {
        if(Root == null)
        {
            Debug.Log("BT_BehaviorTree not populated.");
             yield break;
        }
        while (Application.isPlaying)
        {
            Root.Tick(agent);

            yield return new WaitForSeconds(1.0f/UpdateFrequency);
        }
    }

    #endregion

    #region Test actions

    public void TestBTBasicCompontents(AI_Agent agent)
    {
        int errors = 0;

        #region Standard nodes

        BT_TreeNode f = UDel(failUpdate, "Fail");
        BT_TreeNode s = UDel(succesUpdate, "Succes");
        BT_TreeNode r = UDel(runningUpdate, "Running");
        BT_TreeNode b = UDel(pauseUpdate, "Pause");

        #endregion

        if ((int)agent["Depth"] != 0)
            errors++;

        //SetAgentDebug(true);

        #region Composits: Selector, Sequencer 

        // Check the selector
        errorCheck(sel(f, f, r, s), Status.Running, ref errors, agent);
        errorCheck(sel(f, f, f, f), Status.Failed, ref errors, agent);
        errorCheck(sel(f, f, s, f), Status.Succes, ref errors, agent);

        // Check the sequencer
        errorCheck(seq(s, s, r, f), Status.Running, ref errors, agent);
        errorCheck(seq(s, s, r, s), Status.Running, ref errors, agent);
        errorCheck(seq(f, f, f, f), Status.Failed, ref errors, agent);
        errorCheck(seq(s, s, s, s), Status.Succes, ref errors, agent);

        #endregion

        #region Depth test

        // Generic depth test
        //TestDepth(agent);

        #endregion

        #region Decorators: Invert & alwaysFail

        // Check the inverter
        errorCheck(inv(s), Status.Failed, ref errors, agent);
        errorCheck(inv(f), Status.Succes, ref errors, agent);
        errorCheck(inv(r), Status.Running, ref errors, agent);

        // Check the alwaysFailed
        errorCheck(fail(s), Status.Failed, ref errors, agent);
        errorCheck(fail(f), Status.Failed, ref errors, agent);
        errorCheck(fail(r), Status.Failed, ref errors, agent);

        #endregion

        BT_TreeNode node = new BT_TreeNode(new BT_Decorator());

        #region ReActivate Soon



        #region Condition: CheckEqualBBParameter

        // Things to compare
        int int1 = 0;
        int int2 = 0;
        int int3 = 1;
        string str1 = "bla";
        string str2 = "bla";
        string str3 = "notbla";
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.zero;
        Vector3 v3 = Vector3.up;

        // BB params
        string p1 = "TestParam1";
        string p2 = "TestParam2";
        AI_Agent.BlackBoard local = AI_Agent.BlackBoard.local;
        AI_Agent.BlackBoard global = AI_Agent.BlackBoard.global;

        // Simple int check
        agent[p1, local] = int1;
        agent[p2, local] = int2;

        errorCheck(eqBB(p1, local, p2, local), Status.Succes, ref errors, agent);
        errorCheck(eqBB(p1, local, 0), Status.Succes, ref errors, agent);

        agent[p2, local] = int3;
        errorCheck(eqBB(p1, local, p2, local), Status.Failed, ref errors, agent);
        errorCheck(eqBB(p1, local, 1), Status.Failed, ref errors, agent);

        // cross global and local int check
        agent[p1, global] = int1;
        agent[p2, global] = int3;
        errorCheck(eqBB(p1, local, p1, global), Status.Succes, ref errors, agent);
        errorCheck(eqBB(p1, local, p2, global), Status.Failed, ref errors, agent);

        // string check
        agent[p1, local] = str1;
        agent[p2, local] = str2;

        errorCheck(eqBB(p1, local, p2, local), Status.Succes, ref errors, agent);

        agent[p2, local] = int3;
        errorCheck(eqBB(p1, local, p2, local), Status.Failed, ref errors, agent);

        // cross global and local int check
        agent[p1, global] = str1;
        agent[p2, global] = str3;
        errorCheck(eqBB(p1, local, p1, global), Status.Succes, ref errors, agent);
        errorCheck(eqBB(p1, local, p2, global), Status.Failed, ref errors, agent);

        // Vector3 check
        agent[p1, local] = v1;
        agent[p2, local] = v2;

        errorCheck(eqBB(p1, local, p2, local), Status.Succes, ref errors, agent);

        agent[p2, local] = v3;
        errorCheck(eqBB(p1, local, p2, local), Status.Failed, ref errors, agent);

        // cross global and local int check
        agent[p1, global] = v1;
        agent[p2, global] = v3;
        errorCheck(eqBB(p1, local, p1, global), Status.Succes, ref errors, agent);
        errorCheck(eqBB(p1, local, p2, global), Status.Failed, ref errors, agent);


        #endregion

        #region Action: Copy BB value

        // int copy
        // Check first
        agent[p1, local] = int1;
        agent[p2, local] = int2;

        errorCheck(eqBB(p1, local, p2, local), Status.Succes, ref errors, agent);
        errorCheck(eqBB(p1, local, 0), Status.Succes, ref errors, agent);

        // now copy in 3
        errorCheck(copy(p1, local, 3), Status.Succes, ref errors, agent);
        // Check if it went allright
        errorCheck(eqBB(p1, local, p2, local), Status.Failed, ref errors, agent);
        errorCheck(eqBB(p1, local, 3), Status.Succes, ref errors, agent);

        // now copy from p1 to p2
        errorCheck(copy(p2, local, p1, local), Status.Succes, ref errors, agent);

        // Check
        errorCheck(eqBB(p1, local, p2, local), Status.Succes, ref errors, agent);
        errorCheck(eqBB(p1, local, 3), Status.Succes, ref errors, agent);
        errorCheck(eqBB(p2, local, 3), Status.Succes, ref errors, agent);

        #endregion

        //#region Action: Queue push, pop, checkSize
        //string queueP1 = "TestQueue";
        //string qCompare = "TestQueueComparer";

        //// Create queue and populate
        //fc.Queue<int> queue1 = new fc.Queue<int>();
        //queue1.Add(1);
        //queue1.Add(2);
        //queue1.Add(3);

        //// Populate the board
        //agent[queueP1, local] = queue1;
        //agent[qCompare, local] = -1;

        //// Check size
        //errorCheck(qSize(queueP1, local, 3), Status.Succes, ref errors, agent);

        //// Check push
        //errorCheck(qPush(queueP1, local, 4), Status.Succes, ref errors, agent);
        //errorCheck(qSize(queueP1, local, 4), Status.Succes, ref errors, agent);

        //// Check pop
        //errorCheck(qPop(queueP1, local, qCompare, local), Status.Succes, ref errors, agent);
        //errorCheck(qSize(queueP1, local, 3), Status.Succes, ref errors, agent);
        //errorCheck(eqBB(qCompare, local, 1), Status.Succes, ref errors, agent);
        //errorCheck(qPop(queueP1, local, qCompare, local), Status.Succes, ref errors, agent);
        //errorCheck(eqBB(qCompare, local, 2), Status.Succes, ref errors, agent);
        //errorCheck(qPop(queueP1, local, qCompare, local), Status.Succes, ref errors, agent);
        //errorCheck(eqBB(qCompare, local, 3), Status.Succes, ref errors, agent);
        //errorCheck(qPop(queueP1, local, qCompare, local), Status.Succes, ref errors, agent);
        //errorCheck(eqBB(qCompare, local, 4), Status.Succes, ref errors, agent);
        //errorCheck(qPop(queueP1, local, qCompare, local), Status.Succes, ref errors, agent);
        ////Debug.Log(agent[qCompare, local]);
        //errorCheck(qPop(queueP1, local, qCompare, local), Status.Succes, ref errors, agent);
        //errorCheck(qSize(queueP1, local, 0), Status.Succes, ref errors, agent);

        //#region Test stuff
        ////Queue<int> q = new Queue<int>();
        ////q.Enqueue(6);
        ////q.Enqueue(4);
        ////q.Enqueue(2);

        ////BT_QueuePop whatev = new BT_QueuePop("test", local, "test2", local);
        //////int testooh = (int)whatev.GetFromGenericQueue(q);
        ////Debug.Log((int)whatev.GetFromGenericQueue(q));
        ////Debug.Log((int)whatev.GetFromGenericQueue(q));
        ////Debug.Log((int)whatev.GetFromGenericQueue(q));

        ////List<int> test = new List<int>() { 1, 2, 3 };
        ////IList list = (IList)test;
        ////Debug.Log(list[0]);


        ////fc.Stack<int> stack1 = new fc.Stack<int>();
        ////fc.Stack<string> stack2 = new fc.Stack<string>();
        ////stack1.Add(1);
        ////stack1.Add(2);
        ////stack1.Add(3);

        ////Debug.Log(stack1.GetType().GetGenericTypeDefinition() + " - " + stack1.Get() + " " + stack1.Get() + " " + stack1.Get() + " " + stack1.Get());
        ////fc.Queue<int> queue1 = new fc.Queue<int>();
        ////queue1.Add(1);
        ////queue1.Add(2);
        ////queue1.Add(3);

        ////fc.IQueue queueI = queue1;
        ////Debug.Log(queue1.GetType().GetGenericTypeDefinition() == typeof(fc.Queue<>));
        ////Debug.Log(queue1.GetType().GetGenericTypeDefinition());// + " - " + queue1.Get() + " " + queue1.Get() + " " + queue1.Get() + " " + queue1.Get());
        ////Debug.Log(queueI.GetType().GetGenericTypeDefinition() + " - " + queueI.Get() + " " + queueI.Get() + " " + queueI.Get() + " " + queueI.Get());
        //#endregion

        //#endregion

        #endregion

        Debug.Log("Depth" + (int)agent["Depth"]);
        if ((int)agent["Depth"] != 0)
            errors++;

        if (errors != 0)
            Debug.Log("Behavior Tree test FAILED - " + errors + " Errors." );
        else
            Debug.Log("Behavior Tree test SUCCES - 0 Errors.");
    }

    #region Helpers

    private void SetAgent(AI_Agent.BlackBoard access, string p1, string p2, object obj1, object obj2, AI_Agent agent)
    {
        agent[p1, access] = obj1;
        agent[p2, access] = obj2;
    }

    public void TestDepth(AI_Agent agent)
    {
        BT_TreeNode f = UDel(failUpdate, "Fail");
        BT_TreeNode s = UDel(succesUpdate, "Succes");
        BT_TreeNode r = UDel(runningUpdate, "Running");
        BT_TreeNode b = UDel(pauseUpdate, "Pause");

        BT_TreeNode tree = sel(sel(sel(sel(s, s), s), s), s);

        RebuildTree(tree);
        agent.CheckTreeVersion();

        tree.Tick(agent);
    }

    private void errorCheck(BT_TreeNode root, Status returnStatus, ref int errors, AI_Agent agent)
    {
        RebuildTree(root);
        agent.CheckTreeVersion();
        Status beh = root.Tick(agent);

        // Check if it is the correct return type and if its not invalid
        if (beh != returnStatus)
            errors++;
        if (beh == Status.Invalid)
            errors++;
    }

    #endregion

    #endregion

    #region BT Component Syntactic Sugar

    //private BT_QueuePush qPush(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    //{
    //    return new BT_QueuePush(bbParameter1, param1, bbParameter2, param2);
    //}

    //private BT_QueuePush qPush(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    //{
    //    return new BT_QueuePush(bbParameter1, param1, obj);
    //}

    //private BT_QueuePop qPop(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    //{
    //    return new BT_QueuePop(bbParameter1, param1, bbParameter2, param2);
    //}

    //private BT_QueueCheckSizeEqual qSize(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    //{
    //    return new BT_QueueCheckSizeEqual(bbParameter1, param1, bbParameter2, param2);
    //}

    //private BT_QueueCheckSizeEqual qSize(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    //{
    //    return new BT_QueueCheckSizeEqual(bbParameter1, param1, obj);
    //}

    private BT_TreeNode copy(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        //return new BT_TreeNode(new BT_CopyBBParameter(bbParameter1, param1, bbParameter2, param2));
        //return BT_CopyBBParameter.GetTreeNode();
        return BT_CopyBBParameter.GetTreeNode(bbParameter1, param1, bbParameter2, param2);
    }

    private BT_TreeNode copy(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    {
        return BT_CopyBBParameter.GetTreeNode(bbParameter1,param1, obj);
    }

    //private BT_TreeNode copy(AI_AgentBBAccessParameter param1, object obj)
    //{
    //    return BT_CopyBBParameter.GetTreeNode(param1, obj);
    //}

    private BT_TreeNode eqBB(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
    {
        return BT_CheckEqualBBParameter.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), new AI_AgentBBAccessParameter(bbParameter2, param2));
    }

    private BT_TreeNode eqBB(string bbParameter1, AI_Agent.BlackBoard param1, object obj)
    {
        return BT_CheckEqualBBParameter.GetTreeNode(new AI_AgentBBAccessParameter(bbParameter1, param1), obj);
    }

    private BT_TreeNode fail(BT_TreeNode child)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_AlwayFail());
        node = AddChildren(node, child);
        return node;
    }

    private BT_TreeNode inv(BT_TreeNode child)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_Inverter());
        node = AddChildren(node, child);
        return node;
    }

    private BT_TreeNode sel(params BT_TreeNode[] children)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_Selector());
        node = AddChildren(node, children);
        return node;
    }

    private BT_TreeNode seq(params BT_TreeNode[] children)
    {
        BT_TreeNode node = new BT_TreeNode(new BT_Sequencer());
        node = AddChildren(node,children);
        return node;
    }

    #endregion

    #region Delegator

    private BT_TreeNode UDel(BT_BehaviorDelegator.UpdateDelegate del, string name)
    {
        BT_BehaviorDelegator b = new BT_BehaviorDelegator(BT_Behavior.NodeDescription.BT_NodeType.Action, del);
        b.Description.Name = name;
        BT_TreeNode node = new BT_TreeNode(b);
        return node;
    }

    

    private BT_Behavior.Status failUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Failed;
    }

    private BT_Behavior.Status succesUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Succes;
    }

    private BT_Behavior.Status runningUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Running;
    }

    private BT_Behavior.Status pauseUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        int Depth = (int)agent["Depth"];
        Debug.Break();
        return Status.Succes;
    }

    #endregion

    #region Helpers

    private void SetAgentDebug(bool debug, AI_Agent agent)
    {
        agent.LocalBlackboard.SetObject("DebugTree", debug);
    }

    //public void SetAgent(AI_Agent agent)
    //{
    //    this.agent = agent;

    //    // TODO Setup UI nodes

    //}

    #endregion

    #region Rebuild Tree (recursive)

    private int IDcounter;

    private void RebuildTree(BT_TreeNode root)
    {
        Root = root;
        IDcounter = 0; // Provides the unique id's for this tree by increment
        // Recursive crawl to fill the dictionary
        TreeNodes = new Dictionary<int, BT_TreeNode>();
        TreeNodes = RecursiveTreeNodeCrawl(TreeNodes, root);

        // Set the new tree iteration
        TreeIteration = TreeIteration + 1 % int.MaxValue;

        //Debug.Log("Tree iteration: " + TreeIteration);
    }

    private Dictionary<int, BT_TreeNode> RecursiveTreeNodeCrawl(Dictionary<int, BT_TreeNode> dic, BT_TreeNode node)
    {
        // All the nodes have already a valid parent and children
        // Ids need to be set and unique for this rebuild 
        // Then they need to be added to the new dictionary
        
        // First loop over children recursively
        for (int i = 0; i < node.Children.Count; i++)
        {
            dic = RecursiveTreeNodeCrawl(dic, node.Children[i]);
        }
        //Debug.Log(IDcounter);

        // Set ID
        node.ID = IDcounter;
        
        // Add self to the dictionary
        dic[IDcounter] = node;

        // Increment the counter after adding
        IDcounter++;

        //Debug.Log(IDcounter);

        return dic;
    }

    #endregion

    #region Get children

    public BT_TreeNode GetFirstChild(int index)
    {
        return TreeNodes[index].Children.FirstOrDefault();
    }

    public List<BT_TreeNode> GetChildren(int index)
    {
        return TreeNodes[index].Children;
    }

    #endregion

    #region Set Children

    public BT_TreeNode AddChildren(BT_TreeNode parent, params BT_TreeNode[] children)
    {
        // Check if parents type supports children
        Type type = parent.Behavior.GetType();
        if (!typeof(BT_HasChild).IsAssignableFrom(type))
            Debug.LogError("BT_BehaviorTree.AddChildren adding children is not supported for:" + type.ToString());

        if (typeof(BT_Decorator).IsAssignableFrom(type) && children.Count() > 1)
            Debug.LogError("BT_BehaviorTree.AddChildren adding multiple children is not supported for:" + type.ToString());

        if (children.Count() == 0)
            return parent;

        // Set parents children
        parent.Children.AddRange(children);

        // Set childrens parent
        foreach(BT_TreeNode child in children)
            child.Parent = parent;

        return parent;
    }

    #endregion

    #region Disconnect Children

    #endregion

    internal List<BT_UINode> GetUINodes()
    {
        if (UINodes == null)
            return DefaultUINodeList();

        // Decide how to handle script initilalization of extra tree nodes
        // Decide what to do when there is already ui nodes...
        return UINodes;
    }

    private List<BT_UINode> DefaultUINodeList()
    {
        throw new NotImplementedException();
    }

    #region Connect

    // Add node

    // Remove node

    // Connect (child & parent)

    #endregion

    // Disconnect (child & parent)

    public Dictionary<int, Status> GetNewNodeStatus()
    {
        Dictionary<int, Status> dic = new Dictionary<int, Status>();
        foreach(KeyValuePair<int, BT_TreeNode> node in TreeNodes)
        {
            dic[node.Key] = Status.Invalid;
        }
        return dic;
    }
}
