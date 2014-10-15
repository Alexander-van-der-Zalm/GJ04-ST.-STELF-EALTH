using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

[CustomPropertyDrawer(typeof(AI_AgentBBAccessParameter))]
public class AI_AgentBlackBoardAccessParameterDrawer : PropertyDrawer
{
    const string access = "AgentAccesType";
    const string param = "ParameterName";
    GUIContent accesC = new GUIContent(string.Empty, "Agents Local or Global BlackBoard (storage)");
    GUIContent paramC = new GUIContent(string.Empty, "Paramater Path");

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //AI_AgentBlackBoardAccessParameter param = (AI_AgentBlackBoardAccessParameter)property.val;
        EditorGUI.BeginProperty(position, label, property);

        //label.tooltip = "Agents Local or Global BlackBoard (storage)";

        //Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        Rect accessR = new Rect(position.x, position.y, 50, position.height);
        Rect paramR = new Rect(position.x + 55, position.y, position.width - 55, position.height);

        EditorGUI.PropertyField(accessR, property.FindPropertyRelative(access), GUIContent.none);
        EditorGUI.PropertyField(paramR, property.FindPropertyRelative(param), GUIContent.none);

        EditorGUI.EndProperty();
    }

    public static void CustomDraw(string label, AI_AgentBBAccessParameter bb)
    {
        EditorGUILayout.BeginHorizontal();
        using (new FixedWidthLabel(label))
        {
            int enumIndex = EditorGUILayout.Popup(string.Empty, (int)bb.AgentAccesType, Enum.GetNames(typeof(AI_Agent.BlackBoard)).ToArray(), GUILayout.Width(70));
            var enumValues = (AI_Agent.BlackBoard[])Enum.GetValues(typeof(AI_Agent.BlackBoard));
            bb.AgentAccesType = enumValues[enumIndex];
            bb.ParameterName = EditorGUILayout.TextField(bb.ParameterName);
        }
        //EditorGUILayout.LabelField(label,GUILayout.Width(130));

        
        
        EditorGUILayout.EndHorizontal();
    }
}
