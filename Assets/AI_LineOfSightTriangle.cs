using UnityEngine;
using System.Collections;

public class AI_LineOfSightTriangle : MonoBehaviour 
{
    public GameObject Pivot;
    public Vector3 target, pos;
    public Vector3 dir;
    public void Update()
    {
        RotateTowardsV2(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void RotateTowardsV2(Vector2 target)
    {
        this.target = target;

        pos = Pivot.transform.position;
        dir = this.target - pos;
        dir.z = 0;
        Quaternion newRot = Quaternion.LookRotation(dir,new Vector3(0,1,0));//pos, target);
        Pivot.transform.rotation = newRot;
    }
}
