using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestMono : MonoBehaviour//, ISerializationCallbackReceiver
{
    //public TestDictionary2 Dictionary;
    //public UDictionaryStringFloat Dictionary;
    public UDictionaryStringSerializableObject Dictionary;

    //public TestDictionary test;

    //public UDictionary<string, float> Dictionary;

	// Use this for initialization
	void OnEnable () 
    {
        //if (test == null)
        //{
        //    test = ScriptableObject.CreateInstance<TestDictionary>();
        //    //test.Init();
        //}
	}

    //public void OnAfterDeserialize()
    //{
    //    //Dictionary.OnAfterDeserialize();
    //}

    //public void OnBeforeSerialize()
    //{
    //    //Dictionary.OnBeforeSerialize();
    //}
}
