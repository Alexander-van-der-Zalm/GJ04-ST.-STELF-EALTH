using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]//,RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviour 
{
    public GameObject Sprite;
    public float MaximumVelocity = 10;

    private bool facingLeft = true;

    private Transform tr;
    private Rigidbody2D rb;
    private Animator an;

    private string velXStr = "VelocityX";
    private string velYStr = "VelocityY";
    private string bounceAnimVar = "Bounce";

    private string bounceSoundString = "Bump";
    private int bounceSoundVariations = 1;

    public float BounceTime = 0.3f;

    private Vector2 movementInput = new Vector2();
    

    // Use this for initialization
	void Start () 
    {
        tr = transform;
        rb = rigidbody2D;
        an = GetComponentInChildren<Animator>();
        rb.gravityScale = 0;
        rb.fixedAngle = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        // Change this potentially
        if(!an.GetBool(bounceAnimVar))
            CheckFlipBy(rb.velocity.x);
        // Change this potentially
        rb.velocity = movementInput * MaximumVelocity;

        // Set animator
        an.SetFloat(velXStr, rb.velocity.x);
        an.SetFloat(velYStr, rb.velocity.y);

        
        //HandleZDepth();
        //ResetInput();
	}

    private void ResetInput()
    {
        movementInput = Vector2.zero;
    }

    public void SetInput(float horizontalInput, float verticalInput)
    {
        movementInput.x = horizontalInput;
        movementInput.y = verticalInput;
        if(movementInput.magnitude>1)
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
        Transform tra = tr;
        if (Sprite != null)
            tra = Sprite.transform;
        Vector3 localScale = tra.localScale;
        localScale.x = -localScale.x;
        tra.localScale = localScale;
    }

    /// <summary>
    /// dir positive for right
    /// dir negative for left
    /// </summary>
    /// <param name="dir"></param>
    private void CheckFlipBy(float dir)
    {
        if (dir == 0)
            return;
        dir = Mathf.Sign(dir);

        if (dir > 0 && facingLeft)
            Flip();
        else if (dir < 0 && !facingLeft)
            Flip();
    }

    public void SetBounce()
    {
        StartCoroutine(SetBounceCR());
    }

    private IEnumerator SetBounceCR()
    {
        an.SetBool(bounceAnimVar, true);
        PlayBounceSound();
        CheckFlipBy(-rb.velocity.x);
        yield return new WaitForSeconds(BounceTime);
        an.SetBool(bounceAnimVar, false);
    }

    private void PlayBounceSound()
    {
        int indexNumber = Random.Range(1, bounceSoundVariations+1);
        string sampleName = bounceSoundString + indexNumber;
        Debug.Log(sampleName);
        AudioManager.Play(AudioManager.FindSampleFromCurrentLibrary(sampleName), tr);
    }
}
