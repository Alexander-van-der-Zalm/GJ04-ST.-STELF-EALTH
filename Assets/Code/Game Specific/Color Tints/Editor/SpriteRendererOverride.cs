using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditorInternal;
using System.Reflection;

[CustomEditor(typeof(SpriteRenderer)), CanEditMultipleObjects]
public class SpriteRendererOverride : Editor
{
    string[] sortingLayerNames;

    private ColorTinterMaterialHelper helper;

    private ColorTinterMaterialHelper Helper { get { return helper == null ? helper = ColorTinterMaterialHelper.GetHelper(((SpriteRenderer)target).sharedMaterial) : helper; } }

    void OnEnable()
    {
        sortingLayerNames = GetSortingLayerNames();
        // Get the asset for saving material info before baked into textures etc.
    }

    public override void OnInspectorGUI()
    {
        List<SpriteRenderer> rnds = targets.Cast<SpriteRenderer>().ToList();

        // If no single renderer has any ref to the Color Tinter shader name
        if (rnds.All(r => !r.sharedMaterials.Any(m => m != null && m.name.Contains(ColorTinterMaterialHelper.ShaderNameIncludes))))
        {
            drawOldInspector(rnds);
            return;
        }

        // Check if all are of the custom material Type
        if (rnds.All(r => r.sharedMaterials.Any(m => m != null && m.name.Contains(ColorTinterMaterialHelper.ShaderNameIncludes))))
        {
            drawCustomInspector(rnds);
            return;
        }

        // Do common fields
        drawCommonFields(rnds);
    }

    #region Versions

    private void drawCustomInspector(List<SpriteRenderer> rnds)
    {
        // DefaultFoldOut();

        // Sprite
        ShowSprite(rnds);
        // Material
        ShowMaterial(rnds);
        // Setting: 0.1
        // - Mode(instance): 
        //    - Replace Index Colors(X) - standard sprite (like what u usualy put in game) - color picked values for replacement
        //    - Replace all colors(X) - r channel is colortint - g channel is ao (lighting info) - a is alpha
        //         Seperated channels for info

        // Later termijn - Asset store versie 0.2
        // Diffuse lighting, normals, spec, etc.
        // Je levert een hoop sprites packs (animated en alles) - diffuse versie, normal versie, spec versie
        // -> wij genereren daar een 1 compacte sprite van (met achter de schermen allerlei indices) (op knopje)

        // - Channels(instance): 
        // RampSelector:
        // 1. Prepare sprite - Create asset next to sprite (?)
        // 2. Add palette - Palette is added to material
        // 3. Select ramps - If there is no palette hide
        // Space
        EditorGUILayout.Space();
        // SortingLayer
        ShowSortingLayer(rnds);
        // OrderInLayer
        ShowOrderInLayer(rnds);

        EditorGUILayout.Space();

        ColorTinterMaterialHelper.ReplacementMode mode = ColorTinterMaterialHelper.ReplacementMode.ReplaceColor;

        EditorGUILayout.EnumPopup("Mode", mode);

        // IF no pallete show add pallete to material

        // Else show index and the matching color Palette
        ShowIndexAndPalette(rnds);
    }

    private void drawOldInspector(List<SpriteRenderer> rnds)
    {
        // DefaultFoldOut();

        // Sprite
        ShowSprite(rnds);
        //Color
        ShowColor(rnds);
        //Material
        ShowMaterial(rnds);
        //Space
        EditorGUILayout.Space();
        //SortingLayer
        ShowSortingLayer(rnds);
        //OrderInLayer
        ShowOrderInLayer(rnds);
    }

    private void drawCommonFields(List<SpriteRenderer> rnds)
    {
        //DefaultFoldOut();
        // Sprite
        ShowSprite(rnds);
        //Material
        ShowMaterial(rnds);
        //Space
        EditorGUILayout.Space();
        //SortingLayer
        ShowSortingLayer(rnds);
        //OrderInLayer
        ShowOrderInLayer(rnds);
    }

    #endregion

    #region Shared fields

    private void DefaultFoldOut()
    {
        if (EditorPlus.SavedFoldoutShared("Default", -1, "RendererOverride"))
        {
            EditorGUI.indentLevel++;
            DrawDefaultInspector();
            EditorGUI.indentLevel--;
        }
    }

    private void ShowSprite(List<SpriteRenderer> rnds)
    {
        // CHeck if mixed values
        bool mixedValues = rnds.Select(r => r.sprite).Distinct().Count() > 1;
        Sprite newSprite;

        EditorGUI.BeginChangeCheck();

        if (mixedValues)
        {
            // Mixed values handling
            EditorGUI.showMixedValue = true;
            newSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", null, typeof(Sprite), true);
        }
        else
        {
            // All the same value
            newSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", rnds[0].sprite, typeof(Sprite), true);
        }

        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < rnds.Count; i++)
                rnds[i].sprite = newSprite;
        }

        EditorGUI.showMixedValue = false;
    }

    private void ShowColor(List<SpriteRenderer> rnds)
    {
        // CHeck if mixed values
        bool mixedValues = rnds.Select(r => r.color).Distinct().Count() > 1;
        Color newColor;

        EditorGUI.BeginChangeCheck();

        if (mixedValues)
        {
            // Mixed values handling
            EditorGUI.showMixedValue = true;
            newColor = (Color)EditorGUILayout.ColorField("Color", Color.white);
        }
        else
        {
            // All the same value
            newColor = (Color)EditorGUILayout.ColorField("Color", rnds[0].color);
        }

        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < rnds.Count; i++)
                rnds[i].color = newColor;
        }

        EditorGUI.showMixedValue = false;
    }


    private void ShowMaterial(List<SpriteRenderer> rnds)
    {
        // CHeck if mixed values
        bool mixedValues = rnds.Select(r => r.sharedMaterial).Distinct().Count() > 1;
        Material newMaterial;

        EditorGUI.BeginChangeCheck();

        if (mixedValues)
        {
            // Mixed values handling
            EditorGUI.showMixedValue = true;
            newMaterial = (Material)EditorGUILayout.ObjectField("Material", null, typeof(Material), true);
        }
        else
        {
            // All the same value
            newMaterial = (Material)EditorGUILayout.ObjectField("Material", rnds[0].sharedMaterial, typeof(Material), true);
        }

        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < rnds.Count; i++)
                rnds[i].sharedMaterial = newMaterial;
        }

        EditorGUI.showMixedValue = false;
    }

    private void ShowSortingLayer(List<SpriteRenderer> rnds)
    {
        // CHeck if mixed values
        bool mixedValues = rnds.Select(r => r.sortingLayerID).Distinct().Count() > 1;
        int sortLayerID;

        EditorGUI.BeginChangeCheck();

        if (mixedValues)
        {
            // Mixed values handling
            EditorGUI.showMixedValue = true;
            sortLayerID = (int)EditorGUILayout.Popup("Sorting Layer", 0, sortingLayerNames);
        }
        else
        {
            // All the same value
            sortLayerID = (int)EditorGUILayout.Popup("Sorting Layer", rnds[0].sortingLayerID, sortingLayerNames);
            //sortLayerID = (int)EditorGUILayout.ColorField("Tint", rnds[0].color);
        }

        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < rnds.Count; i++)
                rnds[i].sortingLayerID = sortLayerID;
        }

        EditorGUI.showMixedValue = false;
    }

    private void ShowOrderInLayer(List<SpriteRenderer> rnds)
    {
        // CHeck if mixed values
        bool mixedValues = rnds.Select(r => r.sortingOrder).Distinct().Count() > 1;
        int orderInLayer;

        EditorGUI.BeginChangeCheck();

        if (mixedValues)
        {
            // Mixed values handling
            EditorGUI.showMixedValue = true;
            orderInLayer = (int)EditorGUILayout.IntField("Order in Layer", 0);
        }
        else
        {
            // All the same value
            orderInLayer = (int)EditorGUILayout.IntField("Order in Layer", rnds[0].sortingOrder);
            //sortLayerID = (int)EditorGUILayout.ColorField("Tint", rnds[0].color);
        }

        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < rnds.Count; i++)
                rnds[i].sortingOrder = orderInLayer;
        }

        EditorGUI.showMixedValue = false;
    }

    public string[] GetSortingLayerNames()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        return (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }

    #endregion

    #region Custom Material Specific

    private void ShowIndexAndPalette(List<SpriteRenderer> rnds)
    {
        // Check if there are any palettes
        if(Helper.PaletteCount == 0)
        {
            if(GUILayout.Button("AddNewPalette"))
            {
                Debug.Log("TODO ADD AND DECIDE ON PALETTESIZE");
            }
            return;
        }
        
        // CHeck if mixed values
        bool mixedValues = rnds.Select(r => ColorTinterMaterialHelper.GetPaletteIndex(r)).Distinct().Count() > 1;
        int newIndex;

        EditorGUI.BeginChangeCheck();

        if (mixedValues)
        {
            // Mixed values handling
            EditorGUI.showMixedValue = true;
            newIndex = (int)EditorGUILayout.IntSlider("PaletteIndex", 0, 0, Helper.PaletteCount-1);
        }
        else
        {
            // All the same value
            newIndex = (int)EditorGUILayout.IntSlider("PaletteIndex", ColorTinterMaterialHelper.GetPaletteIndex(rnds[0]),0, Helper.PaletteCount-1);
        }

        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < rnds.Count; i++)
                ColorTinterMaterialHelper.SetPaletteIndex(rnds[i], newIndex);
        }

        Helper.ColorPaletteGUI(newIndex,Screen.width);

        EditorGUI.showMixedValue = false;
    }

    #endregion
}
