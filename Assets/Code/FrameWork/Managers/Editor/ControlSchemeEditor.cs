using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


[CustomEditor(typeof(ControlScheme)),System.Serializable,ExecuteInEditMode]
public class ControlSchemeEditor : EditorPlus 
{
    [SerializeField]
    private List<Type> AllEnums;
    private string[] AllEnumsNames;

    [SerializeField]
    private int selectedIndex;

    string prefID { get { return "ControlSchemeEditor - SelectedEnumIndex[int]:" + target.GetInstanceID().ToString(); } }

    void OnEnable()
    {
        if (AllEnums == null)//Check for assembly reload
        {
            AllEnums = new List<Type>();
            Assembly ass = AssemblyHelper.GetCSharpAssembly();
            
            Type[] types = ass.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].IsEnum && types[i].Name.ToLower().Contains("action"))
                    AllEnums.Add(types[i]);
            }

            // set name list
            AllEnumsNames = AllEnums.Select(e => e.Name).ToArray();
        }

        selectedIndex = EditorPrefs.GetInt(prefID);

        if (selectedIndex >= AllEnums.Count)
            selectedIndex = 0;
    }

    void OnDisable()
    {
        EditorPrefs.SetInt(prefID, selectedIndex);
    }

    public override void OnInspectorGUI()
    {
        #region Original

        if (SavedFoldout("OriginalGUI"))
        {
            EditorGUI.indentLevel++;
            base.OnInspectorGUI();
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }

        #endregion

        //Prep
        ControlScheme ct = (ControlScheme)target;

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        int oldIndex = selectedIndex;
        selectedIndex = EditorGUILayout.Popup(selectedIndex, AllEnumsNames);
        if(GUILayout.Button("SetActions"))
        {
            ct.SetActionsFromEnum(AllEnums[selectedIndex]);
        }
        if (oldIndex!=selectedIndex)
        {
            Debug.Log("Changed");
        }
            
        EditorGUILayout.EndHorizontal();

        #region Horizontal

        if (SavedFoldout("Horizontal Axis"))
        {
            int delete = -1;
            bool add = false;
            for(int i = 0; i < ct.Horizontal.AxisKeys.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (i==0 && GUILayout.Button("+",GUILayout.Width(20.0f)))
                {
                    add = true;
                }
                else if (i!=0 && GUILayout.Button("x", GUILayout.Width(20.0f)))
                {
                    delete = i;
                }
                ct.Horizontal.AxisKeys[i].OnGui();
                GUILayout.EndHorizontal();
            }
            if (delete >= 0)
                ct.Horizontal.AxisKeys.RemoveAt(delete);
            if (add)
                ct.Horizontal.AxisKeys.Add(AxisKey.PC(KeyCode.LeftArrow, KeyCode.RightArrow));
        }

        #endregion

        #region Vertical

        if (SavedFoldout("Vertical Axis"))
        {
            int delete = -1;
            bool add = false;
            for (int i = 0; i < ct.Vertical.AxisKeys.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (i == 0 && GUILayout.Button("+", GUILayout.Width(20.0f)))
                {
                    add = true;
                }
                else if (i != 0 && GUILayout.Button("x", GUILayout.Width(20.0f)))
                {
                    delete = i;
                }
                ct.Vertical.AxisKeys[i].OnGui();
                GUILayout.EndHorizontal();
            }
            if (delete >= 0)
                ct.Vertical.AxisKeys.RemoveAt(delete);
            if (add)
                ct.Vertical.AxisKeys.Add(AxisKey.PC(KeyCode.DownArrow, KeyCode.UpArrow));
        }

        #endregion

        #region Actions

        if (SavedFoldout("Actions"))
        {
           
            for (int i = 0; i < ct.Actions.Count; i++)
            {
                EditorGUI.indentLevel++;
                // For each action - Show a foldout
                if (SavedFoldout(ct.Actions[i].Name))
                {
                    int delete = -1;
                    bool add = false;
                    EditorGUI.indentLevel++;
                    if (ct.Actions[i].Keys.Count == 0 && GUILayout.Button("Add a key"))
                        add = true;

                    for(int j = 0; j < ct.Actions[i].Keys.Count; j++)
                    {
                        GUILayout.BeginHorizontal();

                        if (j == 0 && GUILayout.Button("+", GUILayout.Width(20.0f)))
                        {
                            add = true;
                        }
                        else if (j != 0 && GUILayout.Button("x", GUILayout.Width(20.0f)))
                        {
                            delete = j;
                        }
                        ct.Actions[i].Keys[j].OnGui();

                        GUILayout.EndHorizontal();
                    }
                    EditorGUI.indentLevel--;
                    if (delete >= 0)
                        ct.Actions[i].Keys.RemoveAt(delete);
                    if (add)
                        ct.Actions[i].Keys.Add(ControlKey.PCKey(KeyCode.KeypadEnter));
                }
                EditorGUI.indentLevel--;
            }
        }

        #endregion
    }
}
