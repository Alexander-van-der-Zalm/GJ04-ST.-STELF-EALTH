using UnityEngine;
using System.Collections;

public class MaterialController : MonoBehaviour 
{
    public Color SkinTint, ClothingTint;
    public Color LightTint;
    //public Color FogTint;

    private Transform tr;

   // public Material mat;
    SpriteRenderer renderer;
	// Use this for initialization
	void Start () 
    {
        tr = transform;
        // mat = new Material(GetComponent<SpriteRenderer>().material);
        renderer = GetComponent<SpriteRenderer>();
        //.renderer..color =

	}
	
	// Update is called once per frame
	void Update () 
    {
        renderer.color = FogTintController.GetFogTint(tr.position.y); 

	}
}
