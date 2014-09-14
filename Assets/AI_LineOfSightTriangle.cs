using UnityEngine;
using System.Collections;

public class AI_LineOfSightTriangle : MonoBehaviour 
{
    public GameObject Pivot;
    public bool LOS;

    public Vector2 LastKnowPosition;
    public Vector2 LastKnownDirection;

    public GameObject target;
    private bool lastLOS;

    public void FixedUpdate()
    {
        if (lastLOS && !LOS)
        {
            LastKnowPosition = target.transform.position;
            LastKnownDirection = target.rigidbody2D.velocity;
        }

        //RotateTowardsV2(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lastLOS = LOS;
        LOS = false;
    }

    public void RotateTowardsV2(Vector3 target)
    {
        Vector3 dir = target- Pivot.transform.position;
        dir.z = 0;
        Quaternion newRot = Quaternion.LookRotation(dir,new Vector3(0,1,0));//pos, target);
        Pivot.transform.rotation = newRot;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Player in LOS");
        LOS = true;
        target = other.gameObject;
    }
}
