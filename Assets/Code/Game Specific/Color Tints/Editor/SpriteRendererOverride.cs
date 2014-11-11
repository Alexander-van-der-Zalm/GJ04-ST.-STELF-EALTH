using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SpriteRenderer)), CanEditMultipleObjects]
public class SpriteRendererOverride : Editor
{
    public override void OnInspectorGUI()
    {
        List<SpriteRenderer> rnds = targets.Cast<SpriteRenderer>().ToList();

        // Check if custom material Type
        if (rnds.All(r => r.sharedMaterials.Any(m => m.name.Contains("ColorTinter"))))
        {
            EditorGUILayout.LabelField("CUSTOM INSPECTOR");
            drawCustomInspector(rnds);
            return;
        }

        if (rnds.All(r => !r.sharedMaterials.Any(m => m.name.Contains("ColorTinter"))))
        {
            EditorGUILayout.LabelField("DEFAULT");
            drawOldInspector(rnds);
            return;
        }

        // Do common fields
        EditorGUILayout.LabelField("MULTIPLE MATERIALS");
        drawCommonFields(rnds);
        //base.OnInspectorGUI();

    }

   

    private void drawCustomInspector(List<SpriteRenderer> rnds)
    {
        DefaultFoldOut();
        
        // Sprite
        ShowSprite(rnds);
        // Material
        // Setting:
        // - Mode(instance): Replace Index Colors(X), replace all colors(X)
        // - Channels(instance): 
        // RampSelector:
        // 1. Prepare sprite - Create asset next to sprite
        // 2. Add palette - Palette is added to material
        // 3. Select ramps - If there is no palette hide
        // Space
        // SortingLayer
        // OrderInLayer
    }

    private void drawOldInspector(List<SpriteRenderer> rnds)
    {
        DefaultFoldOut();

        // Sprite
        ShowSprite(rnds);
        //Color
        ShowColor(rnds);
        //Material
        //Space
        //SortingLayer
        //OrderInLayer

        // Replace this
        DrawDefaultInspector();
    }

    private void drawCommonFields(List<SpriteRenderer> rnds)
    {
        DefaultFoldOut();
        // Sprite
        ShowSprite(rnds);
        //Material
        //Space
        //SortingLayer
        //OrderInLayer
    }

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
            newSprite = (Sprite)EditorGUILayout.ObjectField("Sprite",null, typeof(Sprite), true);
        }
        else
        {
            // All the same value
            newSprite = (Sprite)EditorGUILayout.ObjectField("Sprite",rnds[0].sprite, typeof(Sprite), true);
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
            newColor = (Color)EditorGUILayout.ColorField("Tint", Color.white);
        }
        else
        {
            // All the same value
            newColor = (Color)EditorGUILayout.ColorField("Tint", rnds[0].color);
        }

        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < rnds.Count; i++)
                rnds[i].color = newColor;
        }

        EditorGUI.showMixedValue = false;
    }
    
}
