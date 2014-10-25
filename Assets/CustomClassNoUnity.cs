using UnityEngine;
using System.Collections;

[System.Serializable]
public class CustomClassNoUnity
{
    public string StringValue;
    private float FloatValue;

    public CustomClassNoUnity(string StringValue, float FloatValue)
    {
        // TODO: Complete member initialization
        this.StringValue = StringValue;
        this.FloatValue = FloatValue;
    }

    public override string ToString()
    {
        return "CustomClassNoUnity | " + StringValue + " | " + FloatValue;
    }
}
