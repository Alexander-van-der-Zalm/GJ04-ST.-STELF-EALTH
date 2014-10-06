using UnityEngine;
using System.Collections;

public class AI_Agent : MonoBehaviour 
{
    public AI_Blackboard GlobalBlackboard;
    //[HideInInspector]
    private AI_Blackboard LocalBlackboard;

    public BT_BehaviorTree BehaviorTree;

    void Start()
    {
        LocalBlackboard = this.GetOrAddComponent<AI_Blackboard>();

        //TestBlackBoard();
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

        Vector3 v3 = LocalBlackboard.GetObject<Vector3>("TestV3");
        object obj = LocalBlackboard.GetObject("TestV3");
    }
}
