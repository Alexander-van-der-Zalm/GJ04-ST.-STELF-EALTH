﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ColorTinterMaterialHelper : ScriptableObject
{
    #region Enums

    public enum ColorChannel
    {
        R,
        G,
        B,
        A,
        RG,
        GB,
        BA,
        RGB,
        GBA,
        RGBA
    }

    public enum ReplacementMode
    {
        ReplaceColor,
        ReplaceAll
    }

    #endregion

    #region Fields

    [SerializeField]
    private List<ColorPalette> colorPalettes;

    [SerializeField]
    private List<ColorPaletteIndex> ColorPaletteIndices;

    [SerializeField]
    private Material material;

    #endregion

    #region Properties

    //public ColorPalette GetPalette(SpriteRenderer renderer)
    //{
    //    return 
    //}

    //public void SetPalette(SpriteRenderer renderer)
    //{

    //}

    public static string ShaderNameIncludes { get { return "ColorTinter"; } }

    

    #endregion

    #region GetCreateSave Helper

    public static ColorTinterMaterialHelper GetHelper(Material material)
    {
        ColorTinterMaterialHelper helper = (ColorTinterMaterialHelper)AssetDatabase.LoadAssetAtPath(GetHelperPath(material),typeof(ColorTinterMaterialHelper));
        if (helper == null || !AssetDatabase.Contains(helper))
            return Create(material);
        else
            return helper;
    }

    private static string GetHelperPath(Material material)
    {
        string path = AssetDatabase.GetAssetPath(material);
        path = path.Replace(".mat", ".asset");
        Debug.Log(path);
        return path;
    }


    public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
{
    int Place = Source.LastIndexOf(Find);
    string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
    return result;
}

    public static ColorTinterMaterialHelper Create(Material material)
    {
        if (!material.shader.name.Contains(ShaderNameIncludes))
            throw new UnassignedReferenceException("ColorTinterMaterialHelper.Create: the renderer does not use a " + ShaderNameIncludes + " shader");

        if (!AssetDatabase.Contains(material))
            throw new UnassignedReferenceException("ColorTinterMaterialHelper.Create: the material is not stored as an asset");

        // Create the asset
        ColorTinterMaterialHelper helper = ScriptableObject.CreateInstance<ColorTinterMaterialHelper>();
        helper.material = material;
        helper.ColorPaletteIndices = new List<ColorPaletteIndex>();
        helper.colorPalettes = new List<ColorPalette>();

        string path = GetHelperPath(material);
        
        // and add to the material
        AssetDatabase.CreateAsset(helper,path);

        Debug.Log("Created Helper");

        helper.Save();

        return helper;
    }

    public void Save()
    {
        // Save the asset
        //EditorUtility.SetDirty(material);
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    #endregion

    // Maybe move to seperate class?
    #region Renderer helper functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="renderer"></param>
    /// <param name="index"></param>
    /// <returns>Whether the renderer has been set(can fail if it doesnt have the right material/shader)</returns>
    public static bool SetPaletteIndex(SpriteRenderer renderer, int index)
    {
        // Fail if the material has not been properly set
        if (renderer.sharedMaterials.IsNullOrEmpty() || !renderer.sharedMaterials.Any(m => m.shader.name.Contains(ShaderNameIncludes)))
        {
            throw new UnassignedReferenceException("ColorTinterMaterialHelper.SetPaletteIndex: the renderer has no material or does not contain the right shader.");
            return false;
        }


        throw new System.NotImplementedException();


        return false;
    }



    #endregion

    //// Use this for initialization
    //public void SetMaterialIndexHelperTexture()
    //{
    //    // Loop over all the sprites to set 
        
    //    // Set the indexTexture
    //    var texture = new Texture2D(4, 256, TextureFormat.Alpha8, false);
    //    Color[] colors = new Color[4 * 256];
        
    //    // Add colors to the array
    //    for(int x = 0; x < 256; x++)
    //    for(int y = 0; y < 4;y++)
    //    {
    //        colors[y*256+x] = Color.white;// Replace with the sprite info stuffs
    //    }
    //    texture.SetPixels(colors);
        
    //    // ColorRamp?
    //}

    public void TestPopulate()
    {
        colorPalettes = new List<ColorPalette>();
        colorPalettes.Add(new ColorPalette(5));
        colorPalettes[0][0] = new Color(1, 0, 0, 1);
        colorPalettes[0][1] = new Color(1, 1, 0, 1);
        colorPalettes[0][2] = new Color(0, 1, 1, 1);
        colorPalettes[0][3] = new Color(1, .5f, 0, 1);
        colorPalettes[0][4] = new Color(1, 0, 1, 1);

        ColorPaletteIndices = new List<ColorPaletteIndex>();
        ColorPaletteIndices.Add(new ColorPaletteIndex(){Color = new Color(0,0,0,1), Index = 0 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { Color = new Color(30.0f / 255.0f, 0, 0, 1), Index = 1 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { Color = new Color(90.0f / 255.0f, 0, 0, 1), Index = 2 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { Color = new Color(120.0f / 255.0f, 0, 0, 1), Index = 3 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { Color = new Color(150.0f / 255.0f, 0, 0, 1), Index = 4 });
    }

    public void CreateAndSetPaletteTexture()
    {
        int width = 256;
        int height = 1; // Replace by the amount of colorPalettes

        // Create texture
        Texture2D tex = new Texture2D(width,height,TextureFormat.RGBA32,false);
        Color[] colors = new Color[width * height];
        
        // Set default empty non replacing colors
        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
        {
            colors[x + y * width] = new Color(1, 1, 0, 0); // Default empty
        }

        // Texture[versions,spriteIndex] 
        // -> ReplaceColor

        for (int y = 0; y < colorPalettes.Count; y++)
        {
            ColorPalette palette = colorPalettes[y];
            for (int i = 0; i < ColorPaletteIndices.Count; i++)
            {
                
                ColorPaletteIndex indexer = ColorPaletteIndices[i];
                
                // Use the red channel for now
                int spriteIndex = (int)Mathf.Round(indexer.Color.r * 256);
                colors[y * width + spriteIndex] = palette[indexer.Index];

            }
        }
        
        tex.SetPixels(colors);
        tex.name = material.name + "_PaletteTexture";

        // Set shader properties(tex, width, height)
        material.SetTexture("_PaletteTex", tex);
        material.SetInt("_PaletteTexWidth", width);
        material.SetInt("_PaletteTexHeight", height);
    }
}
