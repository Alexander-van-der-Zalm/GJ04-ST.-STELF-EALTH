using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour 
{
    private bool facingLeft = true;

    private Transform tr;
    private Rigidbody2D rb;

    private Vector2 movementInput = new Vector2();
    // Use this for initialization
	void Start () 
    {
        tr = transform;
        rb = rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void SetInput(float horizontalInput, float verticalInput)
    {
        movementInput.x = horizontalInput;
        movementInput.y = verticalInput;
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
