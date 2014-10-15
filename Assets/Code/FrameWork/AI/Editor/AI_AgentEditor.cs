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
        BT_BehaviorTree tree = agent.Tree;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Show Tree"))
            BT_VisualizeTree.ShowTree(tree);
            //tree.Sho
            //Debug.Log("Implement BT_VisualizeTree.Show(tree);");
        if (GUILayout.Button("Hide Tree"))
            Debug.Log("Implement BT_VisualizeTree.Hide();");
        EditorGUILayout.EndHorizontal();
            //target.GenerateBTVisualiser();
    }
}
