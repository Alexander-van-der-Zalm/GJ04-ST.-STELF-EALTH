using UnityEngine;
using System.Collections;
using UnityEditor;

public class ColorTinterMaterialEditor : MaterialEditor 
{
    public override void OnInspectorGUI () 
    {
        if(EditorPlus.SavedFoldoutShared("DefaultInspector",-1,"Inspector"))
            base.OnInspectorGUI ();

        // Show Sprite
        // Do a correct preview

        // Foldout Color Palettes
        // - Show All color Palettes
        // Differ between mem and gpu version
    }
}
