﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AI_Agent))]
public class AI_AgentEditor : EditorPlus 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AI_Agent agent = (AI_Agent)target;
        BT_Tree tree = agent.Tree;

        if (GUI.changed)
        { // Force a selection change
            BTNodeWindowEditor.Instance.SelectionChange();
        }

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Show Tree"))
        {
            BTNodeWindowEditor.ShowWindow();
            BTNodeWindowEditor.Instance.SelectionChange();
        }
            
        if (GUILayout.Button("Hide Tree"))
        {
            BTNodeWindowEditor.HideWindow();
        }

        EditorGUILayout.EndHorizontal();
            //target.GenerateBTVisualiser();
    }
}
