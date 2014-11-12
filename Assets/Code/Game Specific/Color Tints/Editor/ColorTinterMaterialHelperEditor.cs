using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ColorTinterMaterialHelper))]
public class ColorTinterMaterialHelperEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        if (EditorPlus.SavedFoldoutShared("DefaultInspector", -1, "ColorTinterMaterialHelperEditorDefaultInspector"))
        {
            base.OnInspectorGUI();
        }

        ColorTinterMaterialHelper help = (ColorTinterMaterialHelper)target;

        if (EditorPlus.SavedFoldoutShared("ColorTints", -1, "ColorTinterMaterialHelperEditor - ColorPaletteGUI"))
        {
            help.ColorPaletteGUI();
        }

        if (EditorPlus.SavedFoldoutShared("ColorTints", -1, "ColorTinterMaterialHelperEditor - ColorIndexGUI"))
        {
            help.ColorIndexGUI();
        }

        if (GUI.changed)
            help.Save();
    }
}
