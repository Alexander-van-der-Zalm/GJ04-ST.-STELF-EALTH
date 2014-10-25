using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AI_BlackboardComponent : MonoBehaviour 
{
    public AI_Blackboard Blackboard
    {
        get { return blackBoard == null ? blackBoard = AI_Blackboard.Create() : blackBoard; }
        set { Blackboard = value; }
    }

    

    private AI_Blackboard blackBoard;

    //public bool Persist = false;

    //void OnEnable()
    //{
    //    Blackboard.Reconstruct();
    //}

    //void OnDisable()
    //{
    //    Blackboard.PrepareSerialization();
    //}
}
