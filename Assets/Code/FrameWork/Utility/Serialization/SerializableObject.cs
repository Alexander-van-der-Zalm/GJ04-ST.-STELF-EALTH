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
        if(byteArray.Length == 0)
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
            SurrogateHandler.GetOriginal(ref deserializedObject);
        }
        Debug.Log("DEserialized Type: " + deserializedObject.GetType() + " | Value: " + deserializedObject.ToString());
    }

    // Serialize object
    public void OnBeforeSerialize()
    {
        if (deserializedObject == null)
        {
            //Debug.Log("Serialization: deserializedObject is null");
            return;
        }

        Type objType = deserializedObject.GetType();

        // Check surrogates for non serializable types
        if (!objType.IsSerializable)
        {
            if (!SurrogateHandler.GetSurrogate(ref deserializedObject))
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
}
