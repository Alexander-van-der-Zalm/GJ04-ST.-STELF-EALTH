﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class ColorTinterMaterialEditor : MaterialEditor
{
    private ColorTinterMaterialHelper helper;

    //void OnEnable()
    //{
    //    helper = ColorTinterMaterialHelper.GetHelper((Material)target);
    //}

    private ColorTinterMaterialHelper Helper { get { return helper == null ? helper = ColorTinterMaterialHelper.GetHelper((Material)target) : helper; } }

    public override void OnInspectorGUI()
    {
        if (EditorPlus.SavedFoldoutShared("DefaultInspector", -1, "Inspector"))
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
        }

        Material mat = (Material)target;

        // TODO replace bs with actual functionality
        var tex = mat.GetTexture("_PaletteTex");
        if (tex == null)
            EditorGUILayout.LabelField("PaletteTexture and other info not set!");

        #region Test & Bake button

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Test Populate"))
        {
            Helper.TestPopulate();
        }
        if(GUILayout.Button("Bake palettes"))
        {
            Helper.CreateAndSetPaletteTexture();
        }
        EditorGUILayout.EndHorizontal();

        #endregion

        #region Indices
        if (EditorPlus.SavedFoldoutShared("Sprite Color Index", -1, "Sprite Color Index"))
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.IntField(1);
            EditorGUILayout.ColorField(Color.green);
            EditorGUILayout.ColorField(Color.green);
            GUILayout.Button("-");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.IntField(2);
            EditorGUILayout.ColorField(Color.black);
            GUILayout.Button("-");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.IntField(1);
            EditorGUILayout.ColorField(Color.blue);
            GUILayout.Button("-");
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add new index"))
                ColorTinterMaterialHelper.GetHelper((Material)target);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Button("Add indices from grayvalue index picture");
            GUILayout.Button("Clear all");
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region Palettes

        if (EditorPlus.SavedFoldoutShared("Color Palettes", -1, "Color Palettes"))
        {
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.IntField(10);
            //EditorGUILayout.ColorField(Color.green);
            //EditorGUILayout.ColorField(Color.blue);
            //EditorGUILayout.ColorField(Color.yellow);
            //EditorGUILayout.ColorField(Color.red);
            //EditorGUILayout.ColorField(Color.magenta);
            //EditorGUILayout.ColorField(Color.cyan);
            //GUILayout.Button("-");
            //EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.IntField(1);
            //EditorGUILayout.ColorField(Color.red);
            //EditorGUILayout.ColorField(Color.magenta);
            //EditorGUILayout.ColorField(Color.cyan);
            //EditorGUILayout.ColorField(Color.red);
            //EditorGUILayout.ColorField(Color.magenta);
            //EditorGUILayout.ColorField(Color.cyan);
            //GUILayout.Button("-");
            //EditorGUILayout.EndHorizontal();

            Helper.ColorPaletteGUI();

            GUILayout.Button("Add new palette");

            EditorGUILayout.BeginHorizontal();
            GUILayout.Button("Add color palette from grayvalue color palette picture");
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