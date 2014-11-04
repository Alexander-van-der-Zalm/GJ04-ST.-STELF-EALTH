using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Status = BT_Behavior.Status;

[System.Serializable]
public class AI_Agent 
{
    #region Enum

    public enum BlackBoard
    {
        local,
        global
    }

    #endregion

    #region Fields

    // Blackboards to store shared info
    [ReadOnly]
    public string Name = "Default Agent";

    [SerializeField]
    private BT_Tree tree;

    [SerializeField,HideInInspector]
    private AI_Blackboard localBlackboard;

    [SerializeField, HideInInspector]
    private AI_Blackboard globalBlackboard;

    // Stores the status of the tree
    [SerializeField]
    private List<Status> nodeStatus;

    [SerializeField]
    private int TreeVersion = 0;

    //Redo
    private IEnumerator TreeCoroutine;

    #endregion

    #region Properties

    public BT_Tree Tree { get { return tree; } set { tree = value; nodeStatus = tree.GetNodeStatus(); } }
    
    public AI_Blackboard GlobalBlackboard { get { return globalBlackboard; } set { globalBlackboard = value; } }
    
    public AI_Blackboard LocalBlackboard { get { return localBlackboard; } set { localBlackboard = value; } }

    public List<Status> NodeStatus { get { return (nodeStatus == null ? nodeStatus = Tree.GetNodeStatus() : nodeStatus); } set { nodeStatus = value; } }

    public bool TreeRunning { get { return TreeCoroutine != null; } }

    #region Blackboard accessors

    public object this[string name]
    {
        get { return LocalBlackboard.GetObject(name); }
        set { LocalBlackboard.SetObject(name, value); }
    }

    /// <summary>
    /// Get or Set Default in local blackboard
    /// </summary>
    public object GSD(string name, object Default, BlackBoard acces = BlackBoard.local)
    {
        if (acces == BlackBoard.local)
            return LocalBlackboard.GetObjectOrSetDefault(name, Default); 
        else
            return GlobalBlackboard.GetObjectOrSetDefault(name, Default); 
    }

    public object this[string name, BlackBoard acces]
    {
        get { return (acces == BlackBoard.local) ? LocalBlackboard.GetObject(name) : GlobalBlackboard.GetObject(name);}
        set { if(acces == BlackBoard.local) LocalBlackboard.SetObject(name, value); else GlobalBlackboard.SetObject(name, value);}
    }

    //public T GetObjectOrDefault

    #endregion

    public Status this[int index]
    {
        get { return NodeStatus[index]; }
        set { NodeStatus[index] = value; }
    }

    ///// <summary>
    ///// Returns the ID of the child
    ///// </summary>
    //public int this[int nodeIndex, int childIndex]
    //{
    //    get { return childIndex < TreeMemory[nodeIndex].Children.Count ? TreeMemory[nodeIndex].Children[childIndex] : -1; }
    //   // set { TreeMemory[childIndex].Status = value; }
    //}

    #endregion

    #region CreateAgent

    public static AI_Agent CreateAgent(BT_TreeNode Root = null)
    {
        AI_Agent agent = new AI_Agent();
        agent.LocalBlackboard = AI_Blackboard.Create();
        agent.GlobalBlackboard = AI_Blackboard.Create();
        agent.Tree = BT_Tree.CreateTree(Root);

        agent.LocalBlackboard.SetObject("Name", agent.Name);
        agent.LocalBlackboard.SetObject("DebugTree", false);
        agent.LocalBlackboard.SetObject("Depth", 0);

        return agent;
    }
    //// Prep BB

    //// Clear BB
    //public void ClearLocalBlackBoard()
    //{
    //    // Clear local black board
    //    LocalBlackboard.Clear();
    //}

    #endregion

    #region Tick

    public Status TreeTick()
    {
        // Reset Tree (get new nodestatus list)
        nodeStatus = Tree.GetNodeStatus();
        
        // Tick the tree
        return Tree.Root.Tick(this);
    }

    public Status NewTreeTick(BT_TreeNode root)
    {
        Tree = BT_Tree.CreateTree(root);
        return TreeTick();
    }

    #endregion

    #region Tree Start and Pause

    public void CheckTreeVersion()
    {
        if (TreeVersion == Tree.Version)
            return;

        bool treeWasRunning = TreeRunning;
        // Stop the old version of the tree
        //StopTree();

        // TODO unnasign values from global blackboard set by the old tree

        // Get the new status memory structure
        NodeStatus = Tree.GetNodeStatus();
        
        // Update the version
        TreeVersion = Tree.Version;

        // Restart the tree
        //if(TreeRunning)
        //    StartTree();
    }



    //private void StartTree()
    //{
    //    if (Tree == null)
    //    {
    //        Debug.LogError("AI_Agent cant start tree when null");
    //        return;
    //    }
        
    //    TreeCoroutine = Tree.updateCR(this);
    //    StartCoroutine(TreeCoroutine);
    //}

    //private void StopTree()
    //{
    //    if (TreeCoroutine == null)
    //        return;
    //    StopCoroutine(TreeCoroutine);
    //    TreeCoroutine = null;
    //}

    #endregion
}
