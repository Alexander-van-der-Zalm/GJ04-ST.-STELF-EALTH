using UnityEngine;
using System.Collections;
using UnityEditor;

public class ColorTinterMaterialEditor : MaterialEditor
{
    private ColorTinterMaterialHelper helper;

    private ColorTinterMaterialHelper Helper { get { return helper == null ? helper = ColorTinterMaterialHelper.GetHelper((Material)target) : helper; } }

    public override void OnInspectorGUI()
    {
        #region Default layout

        if (EditorPlus.SavedFoldoutShared("DefaultInspector", -1, "Inspector"))
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
        }

        #endregion

        Material mat = (Material)target;

        // TODO replace bs with actual functionality
        var tex = mat.GetTexture("_PaletteTex");
        if (tex == null)
            EditorGUILayout.LabelField("PaletteTexture and other info not set!");

        #region Test & Bake button

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Bake palettes"))
        {
            Helper.CreateAndSetPaletteTexture();
        }
        if (GUILayout.Button("Test Populate"))
        {
            Helper.TestPopulate();
        }
        EditorGUILayout.EndHorizontal();

        #endregion

        #region Indices
        if (EditorPlus.SavedFoldoutShared("Sprite Color Index", -1, "Sprite Color Index"))
        {
            Helper.ColorIndexGUI();
            //EditorGUILayout.BeginHorizontal();
            //GUILayout.Button("Add indices from grayvalue index picture");
            //GUILayout.Button("Clear all");
            //EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region Palettes

        if (EditorPlus.SavedFoldoutShared("Color Palettes", -1, "Color Palettes"))
        {
            Helper.ColorPalettesGUI();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Button("Add new palette");
            //GUILayout.Button("Add color palette from grayvalue color palette picture");
            GUILayout.Button("Clear all");
            EditorGUILayout.EndHorizontal();
        }

        #endregion
        // Show Sprite
        // Do a correct preview

        // Foldout Color Palettes
        // - Show All color Palettes
        // Differ between mem and gpu version

        if(GUI.changed)
        {
            //Helper.CreateAndSetPaletteTexture();
            Helper.Save();
        }
    }
}
