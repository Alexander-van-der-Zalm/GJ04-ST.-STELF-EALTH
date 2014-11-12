using UnityEngine;
using System.Collections;
using Channel = ColorTinterMaterialHelper.ColorChannel;

[System.Serializable]
public class ColorPaletteIndex
{
    public Channel Channel;
    [Range(0,255)]
    public int ColorChannelValue;
    //public Color Color;
    public int Index;
}
