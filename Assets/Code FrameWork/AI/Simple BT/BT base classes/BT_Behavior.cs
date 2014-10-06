using UnityEngine;
using System.Collections;

/// <summary>
/// Base class where Actions, Conditions, Composits: Sequencers and Selectors and Decorators are based on
/// </summary>
public class BT_Behavior
{
    #region Class and enum
    public class NodeDescription
    {
        public enum BT_NodeType
        {
            Action,
            Sequencer,
            Selector,
            Decorator,
            Condition
        }
        public string Name;
        public string Description;
        public BT_NodeType Type;

        // TODO Local n global blackboard parameters

        // TODO Public node paramters
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
    protected AI_Agent Agent;
    private Status status = Status.Invalid;

    #endregion

    #region Constructor

    public BT_Behavior(AI_Agent agent)
    {
        Agent = agent;
    }

    #endregion

    #region virtual functions

    protected virtual Status update() { return Status.Invalid; }
    protected virtual void onInitialize() { }
    protected virtual void onTerminate(Status status) { }

    //public virtual void Instantiate() { }

    #endregion

    #region Tick function

    public Status Tick() 
    {
        // Start if not yet initialized
        if (status == Status.Invalid)
            onInitialize();

        // Update the behaviour
        status = update();

        // Stop if not still running
        if (status != Status.Running)
            onTerminate(status);

        Debug.Log(Description.Type + " - " + Description.Name + " - " + status);

        return status;
    }
    #endregion
}
