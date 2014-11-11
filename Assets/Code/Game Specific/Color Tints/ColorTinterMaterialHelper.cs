using UnityEngine;
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
}
