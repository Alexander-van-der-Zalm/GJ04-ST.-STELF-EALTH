using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AI_BlackboardComponent : MonoBehaviour 
{
    public AI_Blackboard Blackboard;// = new AI_Blackboard();
    //public bool Persist = false;

    void OnEnable()
    {
        Blackboard.Reconstruct();
    }

    void OnDisable()
    {
        Blackboard.PrepareSerialization();
    }
}
