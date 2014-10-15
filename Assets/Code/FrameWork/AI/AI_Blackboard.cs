using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AI_Blackboard 
{
    public string Name = "AI BlackBoard";
    public Dictionary<string, object> objectPool = new Dictionary<string,object>();

    public AI_Blackboard()
    {

    }

    // Default getObject (runs in trouble when it doesnt exist)
    public object this[string name]
    {
        get { return GetObject(name); }
        set { SetObject(name, value); }
    }

    public T GetObject<T>(string name, bool createIfNonexistant = true)
    {
        // Early exit if non-existant
        if (!objectPool.ContainsKey(name))
        {
            if (createIfNonexistant)
                objectPool[name] = default(T);
            else
                return DoesNotContainKey<T>(name);
        }
        return (T)objectPool[name];
    }

    public object GetObject(string name)
    {
        if (!objectPool.ContainsKey(name))
            return DoesNotContainKey<object>(name);

        return objectPool[name];
       // return GetObject<object>(name, createIfNonexistant);
    }

    public object GetObjectOrSetDefault(string name, object newDefault)
    {
        if (objectPool.ContainsKey(name))
            return objectPool[name];

        objectPool[name] = newDefault;
        return newDefault;
    }

    private T DoesNotContainKey<T>(string name)
    {
        Debug.Log("AI_Blackboard.GetObject(" + name + ") does not exist in dictionary. Creating default.");
        return default(T);
    }

    public void SetObject(string name, object obj)
    {
        objectPool[name] = obj;
    }
}
