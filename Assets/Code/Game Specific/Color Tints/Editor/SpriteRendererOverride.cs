using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SpriteRenderer)),CanEditMultipleObjects]
public class SpriteRendererOverride : Editor
{
    public override void OnInspectorGUI()
    {
        List<SpriteRenderer> rnds = targets.Cast<SpriteRenderer>().ToList();
        
        // Check if custom material Type
        if(rnds.All(r => r.sharedMaterials.Any(m => m.name.Contains("ColorTinter"))))
        {
            EditorGUILayout.LabelField("CUSTOM INSPECTOR");
            base.OnInspectorGUI();
            return;
        }

        if (rnds.All(r => !r.sharedMaterials.Any(m => m.name.Contains("ColorTinter"))))
        {
            EditorGUILayout.LabelField("DEFAULT");
            drawOldInspector();
            return;
        }


        EditorGUILayout.LabelField("CANNOT EDIT MULTIPLE DIFFERENTSSS");
        //base.OnInspectorGUI();
        
    }

    private void drawOldInspector()
    {
        //Sprite
        //Color
        //Material
        //Space
        //SortingLayer
        //OrderInLayer
        DrawDefaultInspector();
    }
}
