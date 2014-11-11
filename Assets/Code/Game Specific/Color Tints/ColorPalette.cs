using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorPalette 
{
    [SerializeField,HideInInspector]
    private Color[] colors;

    public int Count { get { return colors.Length; } }
    public Color this[int index] { get { return colors[index]; } set { colors[index] = value; } }

    public ColorPalette(int width)
    {
        colors = new Color[width];
    }

    //public void OnGui()
    //{

    //}
}
