using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

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

    internal void SetScaleFromUnits(float NodeSize)
    {
        float cWidth = (float)Screen.width;
        float iWidth = GetComponent<Image>().preferredWidth;
        
        // Pixel per unit??
        //Camera.ScreenToWorldPoint

        throw new NotImplementedException();
    }
}
