using UnityEngine;
using System;
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

        Debug.Log(objType.Namespace);

        // Check surrogates
        if (!objType.IsSerializable)
        {
            Debug.Log("deserializedObject is null");

        }

        // Serialize
        using(var stream = new MemoryStream())
        {
            var serializer = new BinaryFormatter();
            // SteamingContext?
            serializer.Serialize(stream, deserializedObject);
            byteArray = stream.ToArray();
            Debug.Log("Serialized Type: " + deserializedObject.GetType() + " | Value: " + deserializedObject.ToString());
        }
    }
}
