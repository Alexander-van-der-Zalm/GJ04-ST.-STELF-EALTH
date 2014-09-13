using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform LerpTarget;
    public float CameraSpeed;

    private Vector3 offset;
    private Transform tr;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position;
        tr = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Vector3 target = LerpTarget.position + offset;
        tr.position = Vector3.Lerp(tr.position, target, CameraSpeed * Time.fixedDeltaTime);
	}
}
