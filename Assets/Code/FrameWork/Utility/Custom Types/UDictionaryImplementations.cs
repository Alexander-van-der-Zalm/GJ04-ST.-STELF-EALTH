using UnityEngine;
using System.Collections;

[System.Serializable] public class UDictionaryStringFloat : UDictionary<string,float>{}

[System.Serializable] public class UDictionaryStringSerializableObject : UDictionary<string, SerializableObject> { }

[System.Serializable]public class UDictionaryStringBool : UDictionary<string, bool> { }
