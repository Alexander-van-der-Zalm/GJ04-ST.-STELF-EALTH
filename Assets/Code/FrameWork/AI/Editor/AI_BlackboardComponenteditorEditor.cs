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

        EditorGUILayout.LabelField("[" + bb.ObjectPool.Count + "] Blackboard items: ");

        GUILayout.BeginVertical();
        EditorGUI.indentLevel++;

        List<string> keys = bb.ObjectPool.Keys.ToList();

        foreach (string str in keys)
        {
            bool variableObject = bb.IsVariableObject[str];
            //object original = bb[str];
            object value = EditorField(bb[str], str, false, variableObject);

            // Set value
            bb.SetObject(str, value);
        }

        EditorGUI.indentLevel--;
        GUILayout.EndVertical();

        if (GUILayout.Button("Clear BlackBoard"))
            bb.ObjectPool.Clear();


    }
}