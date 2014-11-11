using UnityEngine;
using System.Collections;
using UnityEditor;

public class ColorTinterMaterialEditor : MaterialEditor 
{
    //private ColorTinterMaterialHelper helper;

    //void OnEnable()
    //{
    //    //helper = ColorTinterMaterialHelper.GetHelper((Material)target);
    //}

    public override void OnInspectorGUI () 
    {
        if(EditorPlus.SavedFoldoutShared("DefaultInspector",-1,"Inspector"))
            base.OnInspectorGUI ();

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

        if (EditorPlus.SavedFoldoutShared("Color Palettes", -1, "Color Palettes"))
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.IntField(10);
            EditorGUILayout.ColorField(Color.green);
            EditorGUILayout.ColorField(Color.blue);
            EditorGUILayout.ColorField(Color.yellow);
            EditorGUILayout.ColorField(Color.red);
            EditorGUILayout.ColorField(Color.magenta);
            EditorGUILayout.ColorField(Color.cyan);
            GUILayout.Button("-");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.IntField(1);
            EditorGUILayout.ColorField(Color.red);
            EditorGUILayout.ColorField(Color.magenta);
            EditorGUILayout.ColorField(Color.cyan);
            EditorGUILayout.ColorField(Color.red);
            EditorGUILayout.ColorField(Color.magenta);
            EditorGUILayout.ColorField(Color.cyan);
            GUILayout.Button("-");
            EditorGUILayout.EndHorizontal();

            GUILayout.Button("Add new palette");

            EditorGUILayout.BeginHorizontal();
            GUILayout.Button("Add color palette from grayvalue color palette picture");
            GUILayout.Button("Clear all");
            EditorGUILayout.EndHorizontal();
        }
        // Show Sprite
        // Do a correct preview

        // Foldout Color Palettes
        // - Show All color Palettes
        // Differ between mem and gpu version
    }
}
