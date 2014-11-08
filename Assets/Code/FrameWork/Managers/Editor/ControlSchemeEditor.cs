using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ControlScheme))]
public class ControlSchemeEditor : EditorPlus 
{
    public override void OnInspectorGUI()
    {
        #region Original

        if (SavedFoldout("OriginalGUI"))
        {
            EditorGUI.indentLevel++;
            base.OnInspectorGUI();
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }

        #endregion

        //Prep
        ControlScheme ct = (ControlScheme)target;

        #region Horizontal

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

        #endregion

        #region Vertical

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

        #endregion

        if (SavedFoldout("Actions"))
        {
            int delete = -1;
            bool add = false;
            for (int i = 0; i < ct.Actions.Count; i++)
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
                if (SavedFoldout(ct.Actions[i].Name))
                {
                    for(int j = 0; j < ct.Actions[i].Keys.Count; j++)
                    {
                        ct.Actions[i].Keys[j].OnGui();
                    }
                }
                
                GUILayout.EndHorizontal();
            }
            if (delete >= 0)
                ct.Vertical.AxisKeys.RemoveAt(delete);
            if (add)
                ct.Vertical.AxisKeys.Add(AxisKey.PC(KeyCode.LeftArrow, KeyCode.RightArrow));
        }
    }
}
