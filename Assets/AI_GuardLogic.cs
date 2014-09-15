using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AI_State_Follow))]
public class AI_GuardLogic : MonoBehaviour 
{
    public AI_UtilityBox UtilityComponents;

    public AI_State_Follow FollowState;
    public AI_State_Patrol AlertedPatrol, NormalPatrol;

    //[ReadOnly]
    public AI_GUARD_STATE State = AI_GUARD_STATE.Normal;
    private AI_State curState;
    private AI_State[] AllStates;

    public enum AI_GUARD_STATE
    {
        Follow = 3,
        Alerted = 2,
        Normal = 0,
        Interupted = 1
    }

	// Use this for initialization
	void Start () 
    {
        //FollowState = new AI_State_Follow();
        FollowState.Create(UtilityComponents);
        //TODO rest of the states
        AllStates = new AI_State[Enum.GetNames(typeof(AI_GUARD_STATE)).Length];
        // new List<AI_State>(Enum.GetNames(typeof(AI_GUARD_STATE)).Length);
        AllStates[(int)AI_GUARD_STATE.Normal] = NormalPatrol;
        AllStates[(int)AI_GUARD_STATE.Alerted] = AlertedPatrol;
        AllStates[(int)AI_GUARD_STATE.Follow] = FollowState;

        State = AI_GUARD_STATE.Normal;
        curState = NormalPatrol;
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

        int newI = (int)newState;
        int oldI = (int)State;
        Debug.Log("Changing to: " + newState + " From: " + State);
        if (AllStates[oldI] == null || AllStates[newI] == null)
            return;
        // Early exit if lower priority and old state not finished
        if (newI < oldI && !AllStates[oldI].IsFinished)
            return;

        Debug.Log("Changing to: " + newState);

        // Disable all states
        foreach(AI_State state in AllStates)
        {
            if (state == null)
                continue;
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
        //curState = 
    }
}
