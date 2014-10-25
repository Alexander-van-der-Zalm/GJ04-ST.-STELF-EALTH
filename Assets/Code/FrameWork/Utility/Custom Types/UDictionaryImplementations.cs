using UnityEngine;
using System.Collections;

[System.Serializable] public class UDictionaryStringFloat : UDictionary<string,float>{}

[System.Serializable] public class ObjectUDictionaryStringSerializable : UDictionary<string, SerializableObject> { }
