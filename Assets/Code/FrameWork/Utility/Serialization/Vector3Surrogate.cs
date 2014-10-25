using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Vector3Surrogate : ISerializationSurrogate 
{
    [SerializeField]
    private float x, y, z;

    public static object GetSurrogateObject(object original)
    {
        Vector3 v3 = (Vector3)original;
        return new Vector3Surrogate() { x = v3.x, y = v3.y, z = v3.z };
    }

    public static object GetOriginalObject(Vector3Surrogate surrogate)
    {
        return surrogate.GetOriginalObject();
    }

    public object GetOriginalObject()
    {
        return new Vector3() { x = this.x, y = this.y, z = this.z };
    }

    public override string ToString()
    {
        return "Vector3Surrogate[" + x + "," + y + "," + z + "]";
    }
}
