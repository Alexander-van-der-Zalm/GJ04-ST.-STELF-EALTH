using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AI_Blackboard))]
public class AI_BlackboardEditor : EditorPlus 
{
    public override void OnInspectorGUI()
    {
        AI_Blackboard bb = (AI_Blackboard)target;

        EditorGUILayout.LabelField(bb.Name);

        GUILayout.BeginVertical();
        EditorGUI.indentLevel++;

        List<string> keys = bb.objectPool.Keys.ToList();

        foreach (string str in keys)
        {
            bb.objectPool[str] = EditorField(bb.objectPool[str], str);
        }

        EditorGUI.indentLevel--;
        GUILayout.EndVertical();
    }
}



