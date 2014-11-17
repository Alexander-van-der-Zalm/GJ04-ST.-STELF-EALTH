using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Iso2DMovementController : ICharacter2DMovement
{
    #region Fields

    [SerializeField]
    public float MaximumVelocity = 1;

    [System.NonSerialized]
    private Vector2 movementInput = new Vector2();

    #endregion

    #region FixedUpdate

    public void FixedPhysicsUpdate(Rigidbody2D rb)
    {
        // Change this potentially
        rb.velocity = movementInput * MaximumVelocity;
    }

    #endregion

    public void SetMovementInput(float horizontalInput, float verticalInput)
    {
        movementInput.x = horizontalInput;
        movementInput.y = verticalInput;
        if (movementInput.magnitude > 1)
            movementInput.Normalize();
    }
}
