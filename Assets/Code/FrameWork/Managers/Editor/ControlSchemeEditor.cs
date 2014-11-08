using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

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

        if (SavedFoldout("Horizontal Axis"))
        {
            int delete = -1;
            bool add = false;
            for(int i = 0; i < ct.Horizontal.AxisKeys.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (i==0 && GUILayout.Button("+",GUILayout.Width(20.0f)))
                {
                    add = true;
                }
                else if (i!=0 && GUILayout.Button("x", GUILayout.Width(20.0f)))
                {
                    delete = i;
                }
                ct.Horizontal.AxisKeys[i].OnGui();
                GUILayout.EndHorizontal();
            }
            if (delete >= 0)
                ct.Horizontal.AxisKeys.RemoveAt(delete);
            if (add)
                ct.Horizontal.AxisKeys.Add(AxisKey.PC(KeyCode.LeftArrow, KeyCode.RightArrow));
        }

        if (SavedFoldout("Vertical Axis"))
        {
            int delete = -1;
            bool add = false;
            for (int i = 0; i < ct.Vertical.AxisKeys.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (i == 0 && GUILayout.Button("+", GUILayout.Width(20.0f)))
                {
                    add = true;
                }
                else if (i != 0 && GUILayout.Button("x", GUILayout.Width(20.0f)))
                {
                    delete = i;
                }
                ct.Vertical.AxisKeys[i].OnGui();
                GUILayout.EndHorizontal();
            }
            if (delete >= 0)
                ct.Vertical.AxisKeys.RemoveAt(delete);
            if (add)
                ct.Vertical.AxisKeys.Add(AxisKey.PC(KeyCode.LeftArrow, KeyCode.RightArrow));
        }
    }
}
