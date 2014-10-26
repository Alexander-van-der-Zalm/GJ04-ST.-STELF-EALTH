using UnityEngine;
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

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Show Tree"))
            BT_VisualizeTree.ShowTree(tree);
        
        if (GUILayout.Button("Hide Tree"))
            BT_VisualizeTree.HideTree();

        EditorGUILayout.EndHorizontal();
            //target.GenerateBTVisualiser();
    }
}
