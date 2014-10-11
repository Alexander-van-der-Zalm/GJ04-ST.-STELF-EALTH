using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class uGuiLockScale : MonoBehaviour
{
    public float RectTransformScale = 1.0f;
    private RectTransform rtr;

    private Vector3 rectScale;


	// Use this for initialization
	void Start () 
    {
        rtr = GetComponent<RectTransform>();
        rectScale = new Vector3();
	}
	
	// Update is called once per frame
	void OnGUI () 
    {
        rectScale.Set(RectTransformScale, RectTransformScale, RectTransformScale);
        rtr.localScale = rectScale;
        //Debug.Log("Called me?");
	}
}
