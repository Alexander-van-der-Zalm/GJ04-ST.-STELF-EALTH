using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class HandleSpriteOrdering : MonoBehaviour 
{
    List<SpriteRenderer> renderers;
    Transform tr;

	// Use this for initialization
	void Start () 
    {
        renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        tr = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    foreach(SpriteRenderer rndr in renderers)
        {
            rndr.sortingOrder = (int)(tr.position.y*-10);
        }
	}
}
