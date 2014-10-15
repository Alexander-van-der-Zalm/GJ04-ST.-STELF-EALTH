using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AI_BlackboardComponent))]
public class AI_BlackboardComponentEditor : EditorPlus
{
    public override void OnInspectorGUI()
    {
        AI_BlackboardComponent bbc = (AI_BlackboardComponent)target;
        AI_Blackboard bb = bbc.Blackboard;

        GUILayout.BeginHorizontal();
        using (new FixedWidthLabel("Name: "))
            bb.Name = EditorGUILayout.TextField(bb.Name);
        //using (new FixedWidthLabel("Persists: "))
        //    bbc.Persist = EditorGUILayout.Toggle(bbc.Persist);
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("[" + bb.objectPool.Count + "] Blackboard items: ");

        GUILayout.BeginVertical();
        EditorGUI.indentLevel++;

        List<string> keys = bb.objectPool.Keys.ToList();

        foreach (string str in keys)
        {
            bb.objectPool[str] = EditorField(bb.objectPool[str], str);
        }

        EditorGUI.indentLevel--;
        GUILayout.EndVertical();

        if (GUILayout.Button("Clear BlackBoard"))
            bb.objectPool.Clear();


    }
}