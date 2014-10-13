using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Linq;

public class AI_Agent : MonoBehaviour 
{
    public enum BlackBoard
    {
        local,
        global
    }
    
    // Blackboards to store shared info
    public AI_Blackboard GlobalBlackboard;
    [HideInInspector]
    public AI_Blackboard LocalBlackboard;
    public BT_BehaviorTree BehaviorTree;
    [ReadOnly]
    public string Name;

    #region Properties

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

    void Start()
    {
        Name = gameObject.name + " " + gameObject.GetInstanceID();

        LocalBlackboard = this.GetOrAddComponent<AI_Blackboard>();
        LocalBlackboard.SetObject("Name", Name);
        LocalBlackboard.SetObject("DebugTree", false);
        LocalBlackboard.SetObject("Depth", 0);


        BehaviorTree.SetAgent(this);
        
        // Test behaviors
        BehaviorTree.TestBTBasicCompontents();
        TestBlackBoard();


        var c = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.IsSubclassOf(typeof(BT_Selector))
                select t.Name.ToString();

        DebugHelper.LogList<string>(c.ToList());

        if (BehaviorTree != null)
            StartCoroutine(BehaviorTree.updateCR(this));
        
        
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
