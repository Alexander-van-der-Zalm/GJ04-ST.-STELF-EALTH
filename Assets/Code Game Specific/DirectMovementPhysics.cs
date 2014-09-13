using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DirectMovementPhysics : MonoBehaviour 
{
    // TODO:
    // - proper accel and deaccel
    // - height maintain (later)
    // - X rotation maintain

    public float Speed;
    public float TurningSpeedAnglePerSecond;

    private Rigidbody rigid;
    private Vector3 newDir;
    private Quaternion rotateToward;
    private Vector3 up;

	void Start () 
    {
        rigid = this.rigidbody;
        up = Vector3.up;
	}

	void FixedUpdate () 
    {
        addVelocity();
        rotate();
        resetSingleUseValues();
	}
    private void addVelocity()
    {
        rigid.velocity = newDir;
    }

    private void rotate()
    {
        if (newDir == Vector3.zero)
            return;

        rotateToward = Quaternion.LookRotation(newDir, up);
        rigid.rotation = Quaternion.RotateTowards(rigid.rotation, rotateToward, TurningSpeedAnglePerSecond * Time.fixedDeltaTime);
    }

    private void resetSingleUseValues()
    {
        newDir = Vector3.zero;
        rotateToward = Quaternion.identity;
    }   
    
    public void Move(Vector2 dir)
    {
        newDir = dir.normalized * Speed;
        newDir.z = newDir.y;
        newDir.y = 0;
    }
}
