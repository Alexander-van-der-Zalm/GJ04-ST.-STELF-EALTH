using UnityEngine;
using System.Collections;

[System.Serializable]
public class CustomClassUnity  
{
    public Vector2 V2;
    public Vector3 V3;

    public CustomClassUnity(Vector3 v3, Vector2 v2)
    {
        V2 = v2;
        V3 = v3;
    }

    public override string ToString()
    {
        return "CustomClassUnity | " + V3.ToString() + " | " + V2.ToString();
    }
}
