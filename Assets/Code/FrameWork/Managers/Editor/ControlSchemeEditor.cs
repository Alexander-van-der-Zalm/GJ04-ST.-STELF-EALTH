using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ControlScheme))]
public class ControlSchemeEditor : EditorPlus 
{
    public override void OnInspectorGUI()
    {
        if (SavedFoldout("OriginalGUI"))
        {
            EditorGUI.indentLevel++;
            base.OnInspectorGUI();
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }

        //Prep
        ControlScheme ct = (ControlScheme)target;

        if (SavedFoldout("Horizontal"))
        {
            for(int i = 0; i < ct.Horizontal.AxisKeys.Count; i++)
            {
                ct.Horizontal.AxisKeys[i].OnGui();
            }
        }
    }
}
