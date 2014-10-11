using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorPlus : Editor
{
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

    protected object EditorField(object value, string label = "", bool labelField = false, params GUILayoutOption[] options)//, GUIContent glabel)
    {
        // Early case if empty
        if (value == null)
        {
            EditorGUILayout.LabelField(label, "null", options);
            return value;
        }
        if(labelField)
        {
            EditorGUILayout.LabelField(label, value.ToString(), options);
            return value;
        }

        object returnvalue = value;

        // AutoHandle by type
        switch(value.GetType().ToString())
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
            default:
                EditorGUILayout.LabelField(label, value.ToString() + " - undifined", options);
                //Debug.Log("EditorPlus.EditorField does not contain definition for " + value.GetType().ToString());
                break;

        }

        return returnvalue;
    }



    #endregion
}
