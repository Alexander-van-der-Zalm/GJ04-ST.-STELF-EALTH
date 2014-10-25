using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SerializableObject : ISerializationCallbackReceiver
{
    [NonSerialized]
    private object deserializedObject;

    [SerializeField]
    private byte[] byteArray;

    //[SerializeField]
    //private UnityEngine.Object unityObject;

    public object Object
    {
        get { return deserializedObject; }
        set { deserializedObject = value; }
    }

    // Deserialize object
    public void OnAfterDeserialize()
    {
        if(byteArray == null)
        {
            Debug.Log("byteArray is null");
            return;
        }

        // Deserialize
        var serializer = new BinaryFormatter();
        using (var stream = new MemoryStream(byteArray))
            deserializedObject = serializer.Deserialize(stream);

        // Check if surrogate and replace
        if(deserializedObject.GetType().GetInterfaces().Contains(typeof(ISerializationSurrogate)))
        {
            GetOriginal(ref deserializedObject);
        }
        Debug.Log("DEserialized Type: " + deserializedObject.GetType() + " | Value: " + deserializedObject.ToString());
    }

    // Serialize object
    public void OnBeforeSerialize()
    {
        if (deserializedObject == null)
        {
            Debug.Log("deserializedObject is null");
            return;
        }

        Type objType = deserializedObject.GetType();

        // Check surrogates
        if (!objType.IsSerializable)
        {
            if(!GetSurrogate(ref deserializedObject))
            {
                Debug.Log("Serialization: object " + objType.ToString() + " is not serializable and has no surrogate");
                return;
            }
        }

        // Serialize
        using(var stream = new MemoryStream())
        {
            var serializer = new BinaryFormatter();
            // SteamingContext?
            serializer.Serialize(stream, deserializedObject);
            byteArray = stream.ToArray();
            //Debug.Log("Serialized Type: " + deserializedObject.GetType() + " | Value: " + deserializedObject.ToString());
        }
    }

    private bool GetSurrogate(ref object surrogate)
    {
        Type objType = surrogate.GetType();

        switch(objType.ToString())
        {
            case "UnityEngine.Vector3":
                surrogate = Vector3Surrogate.GetSurrogateObject(surrogate);
                break;
            default:
                return false;
        }

        return true;
    }

    private bool GetOriginal(ref object original)
    {
        Type objType = original.GetType();

        switch (objType.ToString())
        {
            case "Vector3Surrogate":
                original = Vector3Surrogate.GetOriginalObject((Vector3Surrogate)original);
                break;
            default:
                return false;
        }

        return true;
    }
}
