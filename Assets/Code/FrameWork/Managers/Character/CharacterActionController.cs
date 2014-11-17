using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CharacterActionController : ICharacterActionController
{
    #region Fields

    [SerializeField, HideInInspector]
    private ICharacterAction activeAction;

    /// <summary>
    /// Defines all the actions that are possible (set in Inspector)
    /// </summary>
    [SerializeField]
    private List<ICharacterAction> defaultActions;

    #endregion

    #region Properties

    /// <summary>
    /// Returns false if any active action prevents movement 
    /// </summary>
    public virtual bool CanMove
    {
        get { return !actionPreventsMovement; }
    }

    public List<string> GetDefaultActionNames
    {
        get { return defaultActions.Select(a => a.Name).ToList(); }
    }

    public bool HasActiveAction
    {
        get { return activeAction != null; }
    }

    public ICharacterAction ActiveAction
    {
        get { return activeAction; }
    }

    protected bool actionPreventsMovement
    {
        get { return HasActiveAction && activeAction.CannotMoveDuringAction; }
    }

    protected bool actionUninterruptable
    {
        get { return HasActiveAction && !activeAction.Interuptable; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Creates a new CharacterActionController
    /// </summary>
    /// <param name="DefaultPossibleActions">The default actions that will be invoked if StartAction<T> is used</param>
    public CharacterActionController(List<ICharacterAction> DefaultPossibleActions = null)
    {
        defaultActions = DefaultPossibleActions;
    }

    #endregion

    #region Start Action

    /// <summary>
    /// Checks if the action is possible and then tries to start it
    /// </summary>
    /// <typeparam name="T">Type : ICharacterAction to start</typeparam>
    /// <returns>If the action has started</returns>
    public bool StartAction<T>() where T : ICharacterAction
    {
        // Possibly remove possibleAction from controller:
        // Cannot do the action if there is no possible action
        if (defaultActions.IsNullOrEmpty())
        {
            Debug.LogError("No default action set for " + this.GetType().ToString());
            return false;
        }

        // Select the first of that type
        // Throw an exception if it does not exist in the list
        // For multiple versions of the same type use StartAction(ICharacterAction action)
        ICharacterAction action = defaultActions.Where(a => a.GetType() == typeof(T)).First();

        // Use the standard startAction logic
        return StartAction(action);
    }

    /// <summary>
    /// Tries to start the action
    /// </summary>
    /// <param name="action">Action to start</param>
    /// <returns>If the action has started</returns>
    public bool StartAction(ICharacterAction action)
    {
        // Stop if it cannot be interrupted
        if (actionUninterruptable)
            return false;

        // If it is not currently doing an action start the action
        if (!HasActiveAction)
        {
            return ControllerStartAction(action);
        }

        // Interrupt the currently active action if it is interruptable
        if (action.CanInterupt && StopAction(false))
        {
            return ControllerStartAction(action);
        }

        // Couldn't start the action
        return false;
    }

    protected bool ControllerStartAction(ICharacterAction action)
    {
        action.StartAction(this);
        activeAction = action;
        return true;
    }

    #endregion

    #region Stop Action

    public bool StopAction<T>() where T : ICharacterAction
    {
        if (!HasActiveAction)
            return true;

        if (ActiveAction.GetType() == typeof(T))
        {
            return StopAction(true);
        }

        return false;
    }

    public bool StopAction(ICharacterAction action)
    {
        if (!HasActiveAction)
            return true;

        if (ActiveAction == action)
        {
            return StopAction(true); ;
        }

        return false;
    }

    public bool StopAction(bool overrideInteruptables)
    {
        if (!HasActiveAction)
            return true;
        
        if (overrideInteruptables || activeAction.Interuptable)
        {
            activeAction.StopAction();
            activeAction = null;
            return true;
        }

        return false;
    }

    #endregion
}
