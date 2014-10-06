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


        LocalBlackboard.SetObject("TestV3", new Vector3(12, 1, 2));
        LocalBlackboard.SetObject("TestString", "StringValue");
        LocalBlackboard.SetObject("TestFloat", 0.05f);
        Vector3 v3 = LocalBlackboard.GetObject<Vector3>("TestV3");
        object obj = LocalBlackboard.GetObject("TestV3");
        Debug.Log(v3 + " " + obj.GetType());
    }
}
