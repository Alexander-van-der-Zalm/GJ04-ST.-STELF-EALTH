using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(AI_Blackboard))]
public class AI_BlackboardEditor : EditorPlus 
{

    public override void OnInspectorGUI()
    {
        AI_Blackboard bb = (AI_Blackboard)target;

        EditorGUILayout.LabelField("Local Blackboard");

        GUILayout.BeginVertical();
        EditorGUI.indentLevel++;
        foreach(string str in bb.objectPool.Keys)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(str + " | " + bb.objectPool[str]);
            //SerializedObject obj = new SerializedObject(bb.objectPool[str]);
            //EditorGUILayout.PropertyField()
            GUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel--;
        GUILayout.EndVertical();
        //foreach(string str in )
        //foreach(string str in bb.ObjectPool)

        //base.OnInspectorGUI();
    }
}
