using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorTinterMaterialHelper : MonoBehaviour 
{
    //public enum IndexChannel
    //{
    //    R,
    //    G,
    //    B,
    //    A,
    //    RGBA
    //}
    //public class TintDescription
    //{
    //    public enum Channel
    //    {
    //        R,
    //        G,
    //        B,
    //        A,
    //        RGBA
    //    }
        
    //    public Color SpriteTint;
    //    [Range(0,255)]// BULLSHIT NOODLES
    //    public int Index;

    //}
    
    
    public Material material;

    private List<Sprite> allSpritesWithMaterial;


	// Use this for initialization
	public void SetMaterialIndexHelperTexture()
    {
        // Loop over all the sprites to set 
        
        // Set the indexTexture
        var texture = new Texture2D(4, 256, TextureFormat.Alpha8, false);
        Color[] colors = new Color[4 * 256];
        
        // Add colors to the array
        for(int x = 0; x < 256; x++)
        for(int y = 0; y < 4;y++)
        {
            colors[y*256+x] = Color.white;// Replace with the sprite info stuffs
        }
        texture.SetPixels(colors);
        
        // ColorRamp?
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
