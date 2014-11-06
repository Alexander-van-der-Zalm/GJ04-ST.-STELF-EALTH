using UnityEngine;
using System.Collections;

/// <summary>
/// Base class where Actions, Conditions, Composits: Sequencers and Selectors and Decorators are based on
/// </summary>
[System.Serializable]
public class BT_Behavior:EasyScriptableObject<BT_Behavior>
{
    #region Class and enum
    [System.Serializable]
    public class NodeDescription
    {

        public enum BT_NodeType
        {
            Action,
            Selector,
            Sequencer,
            Decorator,
            Condition
        }

        public string Name = "NotSet";
        public string Description = "IAM DESCRIPTIONORS";

        public BT_NodeType Type = BT_NodeType.Action;

        

        // TODO Public node paramters
        // Seperate out to "Instance" specific - not needed
        //public AI_Blackboard NodeBlackBoard = new AI_Blackboard();

        #region Constructor

         public NodeDescription(BT_NodeType type = BT_NodeType.Action, string name = "NoNameSet", string description = "I need a description.")
        {
            Name = name;
            Type = type;
            Description = description;
            
        }

        #endregion
    }

    public enum Status
    {
        Running,
        Succes,
        Failed,
        Invalid
    }

    #endregion

    #region fields

    public NodeDescription Description = new NodeDescription();
    
    /// <summary>
    /// Current ID (in this tick) Refers to agents list index
    /// </summary>
    [SerializeField,ReadOnly]
    protected int ID;
    
    /// <summary>
    /// Current Agent (in this tick)
    /// </summary>
    [SerializeField]
    protected AI_Agent Agent;

    //private string debugTree = "DebugTree";

    #endregion

    #region virtual inTick functions

    protected virtual Status update() { return Status.Invalid; }
    protected virtual void onInitialize() { }
    protected virtual void onTerminate(Status status) { }

    protected virtual void onEnter() { }

    protected virtual void onExit(Status status) { }

    #endregion

    #region Properties

    /// <summary>
    /// Current active node
    /// </summary>
    protected BT_TreeNode Node { get { return Agent.Tree.TreeNodes[ID]; } }

    #endregion

    #region Tick function

    public Status Tick(AI_Agent agent, int id)
    {
        ID = id;
        Agent = agent;

        // Start if not yet initialized
        if (agent[id] == Status.Invalid)
            onInitialize();

        onEnter();

        // Update the behaviour
        // Save the state to the agent
        Status status = update();
        agent[id] = status;

        onExit(status);

        // Stop if not still running
        if (status != Status.Running)
            onTerminate(status);


        return status;
    }
    #endregion
}
