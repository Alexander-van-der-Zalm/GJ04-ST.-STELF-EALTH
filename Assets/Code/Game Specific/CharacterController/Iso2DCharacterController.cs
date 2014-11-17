using UnityEngine;
using System.Collections;

public class Iso2DCharacterController : Character2DBaseController 
{
    public DanceAction danceAction;

    public Iso2DMovementController Iso2DMovement;

    private AnimationSwapAnimatorWrapper anim;

    #region Properties

    protected override bool CanFlip
    {
        get
        {
            return base.CanFlip;
        }
    }

    #endregion

    #region Constructor

    protected override void Init()
    {
        ActionController = new CharacterActionController();
        if (Iso2DMovement == null)
            Iso2DMovement = new Iso2DMovementController();
        MovementController = Iso2DMovement;
        AnimationController = new AnimatorCollectionWrapper(gameObject);
        anim = new AnimationSwapAnimatorWrapper(gameObject);
        Debug.Log("Start");
    }

    #endregion

    #region FixedUpdate

    protected override void FixedUpdateEnd()
    {
        if (rb.velocity.magnitude > 0)
            anim.Moving = true;
        else
            anim.Moving = false;

        anim.VelocityX = rb.velocity.x;
        anim.VelocityXAbs = Mathf.Abs(rb.velocity.x);
        anim.VelocityY = rb.velocity.y;
    }

    #endregion

    #region Wrap Actions



    public bool Dance()
    {
        return ActionController.StartAction(danceAction, AnimationController);
    }

    public bool StopDance()
    {
        return ActionController.StopAction(danceAction);
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
