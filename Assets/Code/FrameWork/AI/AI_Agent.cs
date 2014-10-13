using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Status = BT_Behavior.Status;

[RequireComponent(typeof(AI_Blackboard))]
public class AI_Agent : MonoBehaviour
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
    public AI_Blackboard GlobalBlackboard;
    [HideInInspector]
    public AI_Blackboard LocalBlackboard;
    public BT_BehaviorTree Tree;
    [ReadOnly]
    public string Name;

    private List<Status> NodeStatus;

    #endregion

    #region Properties

    #region Blackboard accessors

    public object this[string name]
    {
        get { return LocalBlackboard.GetObject(name); }
        set { LocalBlackboard.SetObject(name, value); }
    }

    public object this[string name, BlackBoard acces]
    {
        get { return (acces == BlackBoard.local) ? LocalBlackboard.GetObject(name) : GlobalBlackboard.GetObject(name);}
        set { if(acces == BlackBoard.local) LocalBlackboard.SetObject(name, value); else GlobalBlackboard.SetObject(name, value);}
    }

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

    void Start()
    {
        Name = gameObject.name + " " + gameObject.GetInstanceID();

        LocalBlackboard = this.GetOrAddComponent<AI_Blackboard>();
        LocalBlackboard.SetObject("Name", Name);
        LocalBlackboard.SetObject("DebugTree", false);
        LocalBlackboard.SetObject("Depth", 0);


        //BehaviorTree.SetAgent(this);
        
        // Test behaviors
        Tree.TestBTBasicCompontents(this);
        //TestBlackBoard();


        var c = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.IsSubclassOf(typeof(BT_Selector))
                select t.Name.ToString();

        DebugHelper.LogList<string>(c.ToList());

        if (Tree != null)
            StartCoroutine(Tree.updateCR(this));
        
        
    }

    private void TestBlackBoard()
    {
        LocalBlackboard.SetObject("TestV3", new Vector3(12, 1, 2));
        LocalBlackboard.SetObject("TestString", "StringValue");
        LocalBlackboard.SetObject("TestFloat", 0.05f);
        LocalBlackboard.SetObject("TestInt", 1);
        LocalBlackboard.SetObject("TestV2", new Vector2(1, 2));
        LocalBlackboard.SetObject("TestBool", true);
        LocalBlackboard.SetObject("TestClass2", GlobalBlackboard);
        LocalBlackboard.SetObject("TestClass23", LocalBlackboard);
        LocalBlackboard.SetObject("TestAccesParam", new AI_AgentBBAccessParameter());
        Vector3 v3 = LocalBlackboard.GetObject<Vector3>("TestV3");
        object obj = LocalBlackboard.GetObject("TestV3");
        object obj2 = LocalBlackboard.GetObject("blabla");
    }
}
