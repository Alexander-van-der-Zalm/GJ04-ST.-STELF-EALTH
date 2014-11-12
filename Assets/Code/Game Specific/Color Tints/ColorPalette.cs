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
        int columns = (int)Mathf.Floor(width / minColorWidth); // over x
        int rows = (int)Mathf.Ceil(Count / columns); // over y

        // Height
        return rowHeight * rows;
    }

    public void OnGui(float startX, float startY, float width, float rowHeight, float minColorWidth)
    {
        // Settings
        int colorPickerOverride = 15;

        // Precalculate
        float colf = Mathf.Floor(width / minColorWidth);
        int columns = Mathf.Min(Count,(int)(Mathf.Floor(width / minColorWidth))); // over x
        int rows = (int)(Mathf.Ceil((float)Count / columns)); // over y
        float height = rowHeight * rows;
        float colorWidth = width/columns;

        //// Get StartPos
        Rect rec = new Rect(startX, startY, width, height);
        GUILayoutUtility.BeginGroup("test");

        //Debug.Log(string.Format("Width {0} scrWidth {1}", width, Screen.width));

        //Debug.Log(string.Format("Height {0} Width {1} xy [{2},{3}] rows{6} cols{7} color wh [{4},{5}] min {8}", height, width, startX, startY, colorWidth, rowHeight,rows,columns, minColorWidth));

        if(!rec.Equals(curRec))
        {
            // Set rects
            posRec = new Rect[Count];
            for (int i = 0; i < Count; i++)
            {
                float x = startX + colorWidth * (i % columns);
                float y = startY + rowHeight * (i/Count);
                posRec[i] = new Rect(x, y, colorWidth, rowHeight);
            }
            curRec = rec;
        }

        //for (int i = Count-1; i >= 0; i--)
        //{
        //    Rect copy = new Rect(posRec[i]);
        //    copy.width += colorPickerOverride;
        //    colors[i] = EditorGUI.ColorField(copy, colors[i]);
        //}

        // Draw the Color Field
        for (int i = 0; i < Count; i++)
        {
            //GUI.depth = 
            //colors[i] = 
            //EditorGUIUtility.DrawColorSwatch(posRec[i], colors[i]);//EditorGUI.ColorField(posRec[i], colors[i]);

            colors[i] = EditorGUI.ColorField(posRec[i], colors[i]);
            
        }

        GUILayoutUtility.EndGroup("test");
    }

    #endregion
}
