using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AnimationSwap2Collection))]
public class AnimSwap2CollectionEditor : EditorPlus 
{
    public override void OnInspectorGUI()
    {
        if(SavedFoldout("Achter de schermen"))
        {
            EditorGUI.indentLevel++;
            base.OnInspectorGUI();
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }

        #region Prep

        AnimationSwap2Collection col = (AnimationSwap2Collection)target;
        AnimatorOverrideController over = col.GetNewOverrideController();

        // Calculate maxWidth
        float maxWidth = 0;
        for (int i = 0; i < over.clips.Length; i++)
        {
            float curWidth = GUI.skin.label.CalcSize(new GUIContent(over.clips[i].originalClip.name)).x;
            if (curWidth > maxWidth)
                maxWidth = curWidth;
        }

        #endregion

        // Controller selector
        col.Controller = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Controller",col.Controller, typeof(RuntimeAnimatorController),true);

        // New variety adding
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("NewBodyVariety"))
            col.AddNewBodyVariety();
        if (GUILayout.Button("NewHeadVariety"))
            col.AddNewHeadVariety();
        EditorGUILayout.EndHorizontal();

        if (SavedFoldout("Head varieties",-1))
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < col.HeadVarieties.Count; i++)
            {
                if (SavedFoldout("Head variety[" + i+"]", i))
                {
                    for (int j = 0; j < col.HeadVarieties[i].Animations.Count; j++)
                    {
                        col.HeadVarieties[i].Animations[j].OnGui(over.clips[j].originalClip.name);
                        
                    }
                }
            }
            EditorGUI.indentLevel--;
        }


        if (SavedFoldout("Body varieties", -1))
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < col.BodyVarieties.Count; i++)
            {
                if (SavedFoldout("Body variety[" + i+ "]", i))
                {
                    for (int j = 0; j < col.BodyVarieties[i].Animations.Count; j++)
                    {
                        col.BodyVarieties[i].Animations[j].OnGui(over.clips[j].originalClip.name,maxWidth);
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
    }


}
