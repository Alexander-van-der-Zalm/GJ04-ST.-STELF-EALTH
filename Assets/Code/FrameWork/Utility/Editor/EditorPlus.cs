using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Linq;

public class EditorPlus : Editor
{
    private static Type[] ValidTypes;
    private static string[] ValidTypeStrings;

    #region SavedFoldout

    /// <summary>
    /// Uses EditorPrefs to save and load fold boolean data
    /// </summary>
    internal bool SavedFoldout(GUIContent name, int index = 0, string uniqueID = "")
    {
        string uniqueString = "Fold: " + target.GetInstanceID().ToString() + " - " + uniqueID + index;
        bool fold = EditorPrefs.GetBool(uniqueString, false);
        fold = EditorGUILayout.Foldout(fold, name);
        
        if (GUI.changed)
            EditorPrefs.SetBool(uniqueString, fold);
        
        return fold;
    }

    /// <summary>
    /// Uses EditorPrefs to save and load fold boolean data
    /// </summary>
    internal bool SavedFoldout(string name, int index = -1, string uniqueID = "")
    {
        return SavedFoldout(new GUIContent(name, ""), index, uniqueID);
    }

    /// <summary>
    /// Uses EditorPrefs to save and load fold boolean data
    /// </summary>
    internal bool SavedFoldout(GUIContent name, Rect rect, int index = 0, string uniqueID = "")
    {
        string uniqueString = "Fold: " + target.GetInstanceID().ToString() + " - " + uniqueID + index;
        bool fold = EditorPrefs.GetBool(uniqueString, false);
        fold = EditorGUI.Foldout(rect, fold, name);

        if (GUI.changed)
            EditorPrefs.SetBool(uniqueString, fold);

        return fold;
    }

    /// <summary>
    /// Uses EditorPrefs to save and load fold boolean data
    /// </summary>
    internal bool SavedFoldout(string name, Rect rect, int index = 0, string uniqueID = "")
    {
        return SavedFoldout(new GUIContent(name, ""), rect, index, uniqueID);
    }

    internal void RemoveSavedFoldout(string uniqueID, int index = 0)
    {
        string baseString = "Fold: " + target.GetInstanceID().ToString() + " - " + uniqueID;
        string curString = baseString + index;
        string nextString = baseString + ++index;
        
        while (EditorPrefs.HasKey(nextString))
        {
            bool nextBool = EditorPrefs.GetBool(nextString);
            EditorPrefs.SetBool(curString, nextBool);
            nextString = baseString + ++index;
        }
    }

    #endregion

    #region Draw Every/Alot of Types

    protected object EditorField(object value, string label = "", bool labelField = false, bool isObject = false, params GUILayoutOption[] options)//, GUIContent glabel)
    {
        
        bool horizontal = false;
        string type;
        object returnvalue = value;

        if (ValidTypeStrings==null)
            PopulateTypeArrays();

        // Early case if empty
        if (value == null)
        {
            if (!isObject)
            {
                EditorGUILayout.LabelField(label, "null", options);
                return value;
            }

            //horizontal = true;
            //EditorGUILayout.BeginHorizontal();

            int index = PlayerPrefs.GetInt(label + GetInstanceID().ToString(), 0);
            index = EditorGUILayout.Popup(label,index, ValidTypeStrings);
            PlayerPrefs.SetInt(label + GetInstanceID().ToString(), index);

            type = ValidTypeStrings[index];

            Type selectedType = ValidTypes[index];
            
            if (selectedType.IsValueType)
            {
                value = Activator.CreateInstance(selectedType);
                Debug.Log(value.ToString());
                return value;
            }
            else
            {
                EditorGUILayout.LabelField(label, "null", options);
                return value;
            }
        }
        else
            type = value.GetType().ToString();

        if(labelField)
        {
            EditorGUILayout.LabelField(label, value.ToString(), options);
            return value;
        }

        // AutoHandle by type
        switch (type)
        {
            case "UnityEngine.Vector4":
                returnvalue = EditorGUILayout.Vector4Field(label, (Vector4)value, options);
                break;
            case "UnityEngine.Vector3":
                returnvalue = EditorGUILayout.Vector3Field(label, (Vector3)value, options);
                break;
            case "UnityEngine.Vector2":
                returnvalue = EditorGUILayout.Vector2Field(label, (Vector2)value, options);
                break;
            case "System.Single":
                returnvalue = EditorGUILayout.FloatField(label, (float)value, options);
                break;
            case "System.Int32":
                returnvalue = EditorGUILayout.IntField(label, (int)value, options);
                break;
            case "System.Boolean":
                returnvalue = EditorGUILayout.Toggle(label, (bool)value, options);
                break;
            case "System.String":
                returnvalue = EditorGUILayout.TextField(label, (string)value, options);
                break;
            case "AI_AgentBBAccessParameter":
                AI_AgentBlackBoardAccessParameterDrawer.CustomDraw(label,(AI_AgentBBAccessParameter)value);
                break;
            default:
                EditorGUILayout.LabelField(label, value.ToString() + " - undefined", options);
                Debug.Log("EditorPlus.EditorField does not contain definition for " + value.GetType().ToString());
                break;

        }

        if (horizontal)
            EditorGUILayout.EndHorizontal();

        return returnvalue;
    }

    private void PopulateTypeArrays()
    {
        ValidTypes =  new Type[]{typeof(UnityEngine.Vector4),typeof(UnityEngine.Vector3),typeof(UnityEngine.Vector2),typeof(float),typeof(int),typeof(bool),typeof(string)};
        var strings = from t in ValidTypes
                      select t.ToString();
        ValidTypeStrings = strings.ToArray();
    }

    //string[] types = new string[100](){"UnityEngine.Vector4","UnityEngine.Vector3","UnityEngine.Vector2","System.Single","System.Int32","System.Boolean","System.String","AI_AgentBBAccessParameter"};
    #endregion
}
