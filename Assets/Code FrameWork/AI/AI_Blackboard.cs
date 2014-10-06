using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AI_Blackboard : MonoBehaviour 
{
    Dictionary<string, object> objectPool = new Dictionary<string,object>();

    public T GetObject<T>(string name)
    {
        return (T)objectPool[name];
    }

    public void SetObject(string name, object obj)
    {
        objectPool[name] = obj;
    }
}
