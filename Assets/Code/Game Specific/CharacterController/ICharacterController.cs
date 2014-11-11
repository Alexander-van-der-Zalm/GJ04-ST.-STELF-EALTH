using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Guarantees a moveable controller with the possibility to perform actions
/// </summary>
public interface ICharacterController 
{
    bool CanMove { get; }

    List<string> GetAllPossibleActionNames { get; }

    List<ICharacterAction> ActiveActions { get; }

    /// <summary>
    /// Initiate the movement of the character
    /// </summary>
    /// <param name="horizontalInput"></param>
    /// <param name="verticalInput"></param>
    /// <returns>Succes if can move</returns>
    bool SetMovementInput(float horizontalInput, float verticalInput);
    
    /// <summary>
    /// Try let the character controller do an action
    /// </summary>
    /// <typeparam name="T">A characterAction with ICharacterAction</typeparam>
    /// <returns>Returns if the action has started</returns>
    bool DoAction<T>() where T : ICharacterAction;

    /// <summary>
    /// Stop all active actions with the generic type (even if uninteruptable)
    /// </summary>
    /// <typeparam name="T">Action type to stop : ICharacterAction</typeparam>
    /// <returns>If any actions w</returns>
    bool StopAction<T>() where T : ICharacterAction;

    /// <summary>
    /// Used to stop and remove a specific active action. Always succeeds if the selected action exists (even if uninteruptable).
    /// </summary>
    /// <param name="action">Action instance to stop</param>
    /// <returns>Did the action exist and stop?</returns>
    bool StopAction(ICharacterAction action);

    /// <summary>
    /// Stops all actions 
    /// </summary>
    /// <param name="overrideUninteruptables">If true even stops if the actions are flagged as uninteruptable</param>
    /// <returns>Succes of stopping all the actions</returns>
    bool StopAllActions(bool overrideUninteruptables);
}
