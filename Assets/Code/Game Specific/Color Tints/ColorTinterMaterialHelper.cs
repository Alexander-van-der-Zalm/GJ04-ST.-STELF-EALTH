using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class ColorTinterMaterialHelper : ScriptableObject
{
    #region Enums

    public enum ColorChannel
    {
        R//,
        //G,
        //B,
        //A//,
        //RG,
        //GB,
        //BA,
        //RGB,
        //GBA,
        //RGBA
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

    //public 

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


    public int PaletteCount { get { return colorPalettes.Count; } }

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
       // Debug.Log(path);
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
        if (IsInvalidRenderer(renderer))
        {
            throw new UnassignedReferenceException("ColorTinterMaterialHelper.SetPaletteIndex: the renderer has no material or does not contain the right shader.");
            return false;
        }

        Color newInfo = renderer.color;
        newInfo.r = (float)index / 255;
        renderer.color = newInfo;

        return true;
    }

    public static int GetPaletteIndex(SpriteRenderer renderer)
    {
        // Fail if the material has not been properly set
        if (IsInvalidRenderer(renderer))
        {
            throw new UnassignedReferenceException("ColorTinterMaterialHelper.SetPaletteIndex: the renderer has no material or does not contain the right shader.");
        }

        return (int)(renderer.color.r * 255.0f);
    }

    private static bool IsInvalidRenderer(SpriteRenderer renderer)
    {
        return renderer.sharedMaterials.IsNullOrEmpty() || !renderer.sharedMaterials.Any(m => m.shader.name.Contains(ShaderNameIncludes));
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
        colorPalettes.Add(new ColorPalette());
        colorPalettes[0].Add(new Color(1, 0, 0, 1));
        colorPalettes[0].Add(new Color(1, 1, 0, 1));
        colorPalettes[0].Add( new Color(0, 1, 1, 1));
        colorPalettes[0].Add(new Color(1, .5f, 0, 1));
        colorPalettes[0].Add(new Color(1, 0, 1, 1));

        ColorPaletteIndices = new List<ColorPaletteIndex>();
        ColorPaletteIndices.Add(new ColorPaletteIndex() { ColorChannelValue = 0, Index = 0 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { ColorChannelValue = 30, Index = 1 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { ColorChannelValue = 90, Index = 2 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { ColorChannelValue = 120, Index = 3 });
        ColorPaletteIndices.Add(new ColorPaletteIndex() { ColorChannelValue = 150, Index = 4 });

        Save();
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
                int spriteIndex = indexer.ColorChannelValue;//(int)Mathf.Round(indexer.Color.r * 256);
                colors[y * width + spriteIndex] = palette[indexer.Index];

            }
        }
        
        tex.SetPixels(colors);
        tex.Apply();
        tex.name = material.name + "_PaletteTexture";

        byte[] texData = tex.EncodeToPNG();


        // Create path based on the materials filepath
        string path = AssetDatabase.GetAssetPath(material);
        path = path.Replace(material.name, tex.name);
        path = path.Replace(".mat", ".png");

        // Create the file and import it to the assetDatabase
        File.WriteAllBytes(path, texData);
        
        AssetDatabase.ImportAsset(path);
       // AssetDatabase.Refresh();
        Debug.Log("Created PNG at " + path);

        //Sprite spr = new Sprite();
        //spr.
        Texture matTex = (Texture)AssetDatabase.LoadAssetAtPath(path, typeof(Texture));
        // Set shader properties(tex, width, height)
        material.SetTexture("_PaletteTex", matTex);
        material.SetInt("_PaletteTexWidth", width);
        material.SetInt("_PaletteTexHeight", height);
    }

    #region GUI

    private int colorHeight = 15;
    private float minColorWidht = 45;

    private int buttonwidth = 20;
    private int indexwidth = 30;

    private float width;
    

    public void ColorPalettesGUI()
    {
        width = Screen.width;
        float colorPaletteWidth = width - buttonwidth - indexwidth - 20;

        for(int i = 0; i < colorPalettes.Count; i++)
        {
            float height = colorPalettes[i].GUIHeight(colorPaletteWidth, colorHeight, minColorWidht);
            
            EditorGUILayout.BeginHorizontal();
            {
                // ID
                if (GUILayout.Button("-", GUILayout.Width(buttonwidth), GUILayout.MinWidth(buttonwidth), GUILayout.MaxWidth(buttonwidth)))
                {
                    Debug.Log("Delete" + i);
                }

                EditorGUILayout.LabelField("ID:" + i, GUILayout.Width(indexwidth), GUILayout.MinWidth(indexwidth), GUILayout.MaxWidth(indexwidth));

                // Palette
                Rect rec = GUILayoutUtility.GetRect(width, height);
                colorPalettes[i].OnGui(rec.x, rec.y, colorPaletteWidth, colorHeight, minColorWidht);

                // Delete or add button
                
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    public void ColorPaletteGUI(int index, float colorPaletteWidth)
    {
        width = Screen.width;
        float height = colorPalettes[index].GUIHeight(colorPaletteWidth, colorHeight, minColorWidht);
        colorPaletteWidth = width;

        Rect rec = GUILayoutUtility.GetRect(width, height);
        colorPalettes[index].OnGui(rec.x, rec.y, colorPaletteWidth, colorHeight, minColorWidht);
    }

    public void ColorIndexGUI()
    {
        float width = Screen.width;

        for(int i = 0; i < ColorPaletteIndices.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            ColorPaletteIndices[i].Index = (int)Mathf.Clamp(EditorGUILayout.IntField("PaletteIndexer", ColorPaletteIndices[i].Index), 0, 255);
            ColorPaletteIndices[i].Channel = (ColorChannel)EditorGUILayout.EnumPopup(ColorPaletteIndices[i].Channel);
            ColorPaletteIndices[i].ColorChannelValue = (int)Mathf.Clamp(EditorGUILayout.IntField(ColorPaletteIndices[i].ColorChannelValue), 0, 255);
                
            EditorGUILayout.EndHorizontal();
        }
    }

    #endregion
}
