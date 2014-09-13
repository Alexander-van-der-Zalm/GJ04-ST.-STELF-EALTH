using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour 
{
    public float MaximumVelocity = 10;

    private bool facingLeft = true;

    private Transform tr;
    private Rigidbody2D rb;

    public Vector2 movementInput = new Vector2();

    // Use this for initialization
	void Start () 
    {
        tr = transform;
        rb = rigidbody2D;
        rb.gravityScale = 0;
        rb.fixedAngle = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        rb.velocity = movementInput * MaximumVelocity;
        HandleZDepth();
	}

    public void SetInput(float horizontalInput, float verticalInput)
    {
        movementInput.x = horizontalInput;
        movementInput.y = verticalInput;
        movementInput.Normalize();
    }

    private void HandleZDepth()
    {
        Vector3 pos = tr.position;
        pos.z = pos.y;
        tr.position = pos;
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 localScale = tr.localScale;
        localScale.x = -localScale.x;
        tr.localScale = localScale;
    }
}
