using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Rigidbody2D))]//,RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviour
{
    #region Fields

    //public GameObject Body;
    public float MaximumVelocity = 10;

    private bool facingLeft = true;

    private Transform tr;
    private Rigidbody2D rb;
    private AnimationSwapAnimatorWrapper anim;

    private string bounceSoundString = "Bump";
    private int bounceSoundVariations = 1;

    public float BounceTime = 0.3f;

    private Vector2 movementInput = new Vector2();

    #endregion

    // Use this for initialization
	void Start () 
    {
        tr = transform;
        rb = GetComponentInChildren<Rigidbody2D>();
        anim = new AnimationSwapAnimatorWrapper(gameObject);
        //an = GetComponentInChildren<Animator>();
        rb.gravityScale = 0;
        rb.fixedAngle = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        // Change this potentially
        if(!anim.Bump)
            CheckFlipBy(rb.velocity.x);
        
        // Change this potentially
        rb.velocity = movementInput * MaximumVelocity;

        // Set animator
        if (rb.velocity.magnitude > 0)
            anim.Moving = true;
        else
            anim.Moving = false;

        anim.VelocityX = rb.velocity.x;
        anim.VelocityXAbs = Mathf.Abs(rb.velocity.x);
        anim.VelocityY = rb.velocity.y;
        //an.SetFloat(velXStr, rb.velocity.x);
        //an.SetFloat(velYStr, rb.velocity.y);

        
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

    private void Flip()
    {
        facingLeft = !facingLeft;
        Transform tra = tr;
        //if (Body != null)
        //    tra = Body.transform;
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
        //an.SetBool(bounceAnimVar, true);
        anim.Bump = true;
        PlayBounceSound();
        CheckFlipBy(-rb.velocity.x);
        yield return new WaitForSeconds(BounceTime);
        anim.Bump = false;
        //an.SetBool(bounceAnimVar, false);
    }

    private void PlayBounceSound()
    {
        int indexNumber = Random.Range(1, bounceSoundVariations+1);
        string sampleName = bounceSoundString + indexNumber;
        Debug.Log("PlayBounceSound: "+sampleName);
        AudioManager.Play(AudioManager.FindSampleFromCurrentLibrary(sampleName), tr);
    }
}
