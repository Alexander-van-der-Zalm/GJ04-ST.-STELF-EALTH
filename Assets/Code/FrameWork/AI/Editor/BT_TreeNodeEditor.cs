using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BT_TreeNode))]
public class BT_TreeNodeEditor : EditorPlus 
{
    public override void OnInspectorGUI()
    {
        BT_TreeNode node = (BT_TreeNode)target;
        //node.name = node.Behavior.Description.Name.Substring(0,3) + " " + node.Name.Substring(0,6) + " ["+node.ID+"]";
        EditorGUILayout.LabelField("NodeName: ", node.name);
        node.Name = EditorGUILayout.TextField("Identifying name:",node.Name);

        EditorGUILayout.Space();

        AI_BlackboardComponentEditor.BlackBoardGUI(node.ParametersBB);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear BlackBoard"))
            node.ParametersBB.Clear();
        if (GUILayout.Button("Reset Behavior Parameters"))
            node.SetBehaviorParameters();
        //if (GUILayout.Button("Decouple"))
        //{
        //    node.ParametersBB = AI_Blackboard.CreateShallowCopy(node.ParametersBB);
        //}


    }
}
