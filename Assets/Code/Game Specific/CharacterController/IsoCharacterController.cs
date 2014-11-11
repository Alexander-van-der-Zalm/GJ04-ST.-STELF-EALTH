using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsoCharacterController : MonoBehaviour, ICharacterController
{
    #region Fields

    #endregion

    #region Properties

    public List<string> GetAllPossibleActionNames
    {
        get { throw new System.NotImplementedException(); }
    }

    public List<ICharacterAction> ActiveActions
    {
        get { throw new System.NotImplementedException(); }
    }

    #endregion

    #region Start

    // Use this for initialization 
	void Start () {
	
	}

    #endregion

    #region Update

    // Update is called once per frame
	void Update () {
	
	}

    #endregion

    #region ICharacterController

    public bool SetMovementInput(float horizontalInput, float verticalInput)
    {
        throw new System.NotImplementedException();
    }

    public bool DoAction<T>() where T : ICharacterAction
    {
        throw new System.NotImplementedException();
    }

    public bool StopAllActions(bool overrideInteruptables)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public bool CanMove
    {
        get { throw new System.NotImplementedException(); }
    }




    public bool StopAction<T>() where T : ICharacterAction
    {
        throw new System.NotImplementedException();
    }

    public bool StopAction(ICharacterAction action)
    {
        throw new System.NotImplementedException();
    }
}
