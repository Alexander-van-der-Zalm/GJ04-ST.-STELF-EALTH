using UnityEngine;
using System.Collections;
using UnityEditor;

public class ColorTinterMaterialEditor : MaterialEditor 
{
    public override void OnInspectorGUI () 
    {
        EditorGUILayout.LabelField("TEST");
        base.OnInspectorGUI ();
    }
}
