using UnityEngine;
using System.Collections;

public class Iso2DCharacterController : Character2DBaseController 
{
    public DanceAction danceAction;
    
    protected override bool CanFlip
    {
        get
        {
            return base.CanFlip;
        }
    }
    
    protected override void Init()
    {
        ActionController = new CharacterActionController();
        MovementController = new Iso2DMovementController();
    }

    #region Wrap Actions

    public bool Dance()
    {
        return ActionController.StartAction(danceAction);
    }

    public bool PickPocket()
    {
        throw new System.NotImplementedException();
    }

    public bool Bounce()
    {
        throw new System.NotImplementedException();
    }

    public bool Talk()
    {
        throw new System.NotImplementedException();
    }

    

    #endregion
}
