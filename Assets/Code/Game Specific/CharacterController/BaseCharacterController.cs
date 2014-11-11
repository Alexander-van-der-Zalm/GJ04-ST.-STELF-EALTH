using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// TODOOOO
// X - Think about if and how multiple actions are handled
// X - Finish stopaction

/// <summary>
/// Can be inherented from to avoid reimplementing a basic action handling system.
/// Does not implement everything (like movement) marked virtual.
/// </summary>
[System.Serializable]
public class BaseCharacterController : MonoBehaviour , ICharacterController
{
    #region Fields

    [SerializeField,HideInInspector]
    protected List<ICharacterAction> activeActions;

    /// <summary>
    /// Defines all the actions that are possible (set in Inspector)
    /// </summary>
    [SerializeField]
    protected List<ICharacterAction> PossibleActions;

    #endregion

    void Start()
    {
        Debug.LogError("This class should not be used as an component");
    }

    #region Properties

    /// <summary>
    /// Returns false if any active action prevents movement 
    /// </summary>
    public virtual bool CanMove
    {
        get { return !actionPreventsMovement; }
    }

    public List<string> GetAllPossibleActionNames
    {
        get { return PossibleActions.Select(a => a.Name).ToList(); }
    }

    public List<ICharacterAction> ActiveActions
    {
        get { return activeActions; }
    }

    protected bool actionPreventsMovement
    {
        get { return activeActions.Any(k => k.CannotMoveDuringAction); }
    }

    protected bool actionUninterruptable
    {
        get { return activeActions.Any(k => !k.Interuptable); }
    }

    #endregion

    #region Not Implemented

    public virtual bool SetMovementInput(float horizontalInput, float verticalInput)
    {
        throw new System.NotImplementedException();
    }

   
    #endregion

  

    #region Start & Stop Action

    public bool DoAction<T>() where T : ICharacterAction
    {
        // Cannot do the action if there is no possible action
        if (PossibleActions.IsNullOrEmpty())
        {
            Debug.LogError("No possible actions set for " + this.GetType().ToString());
            return false;
        }

        // Stop if it cannot be interrupted
        if (actionUninterruptable)
            return false;

        // Select the first of that type (no multiple versions of the same type supported (yet))
        // Throw an exception if it does not exist in the list
        ICharacterAction action = PossibleActions.Where(a => a.GetType() == typeof(T)).First();

        // Think about interruption and multi actions

        // If it can interupt 
        if(action.CanInterupt)
        {
            // Interrupt others??
            StartAction(action);
            return true;
        }

        // If it is empty start the action
        if(!activeActions.IsEmpty())
        {
            StartAction(action);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Starts and adds the action to the list
    /// </summary>
    /// <param name="action">Action to start and add</param>
    protected void StartAction(ICharacterAction action)
    {
        action.StartAction(this);
        activeActions.Add(action);
    }

    public bool StopAction<T>() where T : ICharacterAction
    {
        throw new System.NotImplementedException();
    }

    public bool StopAction(ICharacterAction action)
    {
        throw new System.NotImplementedException();
    }

    public bool StopAllActions(bool overrideInteruptables)
    {
        bool succes = true;
        for(int i = 0; i < activeActions.Count; i++)
        {
            if (overrideInteruptables || activeActions[i].Interuptable)
                activeActions[i].StopAction();
            else
                succes = false;
        }
        return succes;
    }

    #endregion

    
}
