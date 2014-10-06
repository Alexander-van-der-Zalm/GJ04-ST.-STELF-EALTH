using UnityEngine;
using System.Collections;

public class AI_Agent : MonoBehaviour 
{
    public AI_Blackboard GlobalBlackboard;
    //[HideInInspector]
    public AI_Blackboard LocalBlackboard;

    public BT_BehaviorTree BehaviorTree;

    void Start()
    {
        LocalBlackboard = new AI_Blackboard();

        LocalBlackboard.SetObject("TestV3", new Vector3(12, 1, 2));
        Vector3 v3 = LocalBlackboard.GetObject<Vector3>("TestV3");
        Debug.Log(v3);
    }
}
