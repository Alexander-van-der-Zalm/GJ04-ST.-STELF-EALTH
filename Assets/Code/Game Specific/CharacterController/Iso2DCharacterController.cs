using UnityEngine;
using System.Collections;

public class Iso2DCharacterController : Character2DBaseController 
{
    //public 
    
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

    
}
