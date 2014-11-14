using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class ColorPalette 
{
    [SerializeField]
    private List<Color> colors;

    public int Count { get { return colors.Count; } }
    public Color this[int index] { get { return colors[index]; } set { colors[index] = value; } }

    [System.NonSerialized]
    private Rect curRec;

    [System.NonSerialized]
    private Rect[] posRec;

    public ColorPalette()
    {
        colors = new List<Color>();
    }

    public void Add(Color color)
    {
        colors.Add(color);
    }

    #region GUI

    public float GUIHeight(float width, float rowHeight, float minColorWidth)
    {
        int columns = Mathf.Min(Count, (int)(Mathf.Floor(width / minColorWidth))); // over x
        int rows = (int)(Mathf.Ceil((float)Count / columns)); // over y

        // Height
        return rowHeight * rows;
    }

    public void OnGui(float startX, float startY, float width, float rowHeight, float minColorWidth)
    {
        // Precalculate
        float colf = Mathf.Floor(width / minColorWidth);
        int columns = Mathf.Min(Count,(int)(Mathf.Floor(width / minColorWidth))); // over x
        int rows = (int)(Mathf.Ceil((float)Count / columns)); // over y
        float height = rowHeight * rows;
        float colorWidth = width/columns;

        //// Get StartPos
        Rect rec = new Rect(startX, startY, width, height);
        GUILayoutUtility.BeginGroup("test");

        if(!rec.Equals(curRec))
        {
            // Set rects
            posRec = new Rect[Count];
            for (int i = 0; i < Count; i++)
            {
                float x = startX + colorWidth * (i % columns);
                float y = startY + (rowHeight + 2)* ((i) / columns);
                posRec[i] = new Rect(x, y, colorWidth, rowHeight);
            }
            curRec = rec;
        }

        // Draw the Color Field
        for (int i = 0; i < Count; i++)
        {
            //Replace with: EditorGUIUtility.DrawColorSwatch(posRec[i], colors[i]); ??
            colors[i] = EditorGUI.ColorField(posRec[i], colors[i]);
        }

        GUILayoutUtility.EndGroup("test");
    }

    #endregion
}
