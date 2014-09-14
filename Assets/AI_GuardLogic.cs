using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AI_State_Follow))]
public class AI_GuardLogic : MonoBehaviour 
{
    public AI_UtilityBox UtilityComponents;

    public AI_State_Follow FollowState;
    public AI_State_Patrol AlertedPatrol, NormalPatrol;

    //[ReadOnly]
    public AI_GUARD_STATE State = AI_GUARD_STATE.Normal;

    private List<AI_State> AllStates = new List<AI_State>();

    public enum AI_GUARD_STATE
    {
        Follow,
        Alerted,
        Normal,
        Interupted
    }

	// Use this for initialization
	void Start () 
    {
        //FollowState = new AI_State_Follow();
        FollowState.Create(UtilityComponents);
        //TODO rest of the states
        AllStates.Add(FollowState);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //Maintain states
        if (UtilityComponents.Los.LOS)
            ChangeState(AI_GUARD_STATE.Follow);
        else if (UtilityComponents.Alert.Alerted)
            ChangeState(AI_GUARD_STATE.Alerted);
        else
            ChangeState(AI_GUARD_STATE.Normal);
	}

    private void ChangeState(AI_GUARD_STATE newState)
    {
        // Early exit if not actually changing
        if (newState == State)
            return;

        Debug.Log("Changing to: " + newState);

        // Disable all states
        foreach(AI_State state in AllStates)
        {
            state.Pause();
        }

        //TODO SWITCH OTHERS
        switch(newState)
        {
            case AI_GUARD_STATE.Follow:
                FollowState.Resume();
                break;
            default:
                break;
        }

        // Save current state
        State = newState;
        
    }
}
