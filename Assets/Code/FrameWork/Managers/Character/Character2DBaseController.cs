using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// TODOOOO
// V - Think about if and how multiple actions are handled - 1 Action mAx
// V - Finish stopaction
// V - ICharacterController -> IActionController
// V - Base implements an actionController (also possible to have multipe action controllers (run n gun)
// X - Implementation has a list of exposeable actions binded to the metalogic described in the character controllers to easily make variations (like Jump, run, etc)
// X - Think about how to change movement from action (Fraction multiplier)
// X - Think about how to initiate movement from action (override input?)
// X - Think about how to prevent Flip from action (expose to action?)

/// <summary>
/// Can be inherented from to avoid reimplementing a basic flip
/// </summary>
[System.Serializable, RequireComponent(typeof(Rigidbody2D))]
public class Character2DBaseController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private ICharacterActionController ActionController;

    [SerializeField]
    private ICharacter2DMovement MovementController;

    [System.NonSerialized]
    private Rigidbody2D rb;

    [System.NonSerialized]
    private Transform tr;

    [System.NonSerialized]
    private bool facingLeft;

    #endregion

    #region Properties

    protected virtual bool CanFlip { get { return true; } }

    #endregion

    #region Start

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        tr = gameObject.GetComponent<Transform>();
        Init();
    }

    protected virtual void Init()
    {
        if (ActionController == null || MovementController == null)
            throw new System.NullReferenceException("Character2DBaseController.Init movement or action controller not set");
    }

    #endregion

    #region Fixed physics update

    void FixedUpdate()
    {
        FixedUpdateStart();
        
        if (CanFlip)
            CheckFlipBy(rb.velocity.x);

        if (ActionController.CanMove)
            MovementController.FixedPhysicsUpdate(rb);

        FixedUpdateEnd();
    }

    protected virtual void FixedUpdateEnd()
    {

    }

    protected virtual void FixedUpdateStart()
    {

    }

    #endregion

    private void Flip()
    {
        facingLeft = !facingLeft;

        Vector3 localScale = tr.localScale;
        localScale.x = -localScale.x;
        tr.localScale = localScale;
    }

    /// <summary>
    /// dir positive for right
    /// dir negative for left
    /// </summary>
    /// <param name="dir"></param>
    protected void CheckFlipBy(float dir)
    {
        if (dir == 0)
            return;
        dir = Mathf.Sign(dir);

        if (dir > 0 && facingLeft)
            Flip();
        else if (dir < 0 && !facingLeft)
            Flip();
    }

}
