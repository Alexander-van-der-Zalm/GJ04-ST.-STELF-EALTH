using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AI_Blackboard 
{
    public string Name = "AI BlackBoard";
    public Dictionary<string, object> ObjectPool;
    public Dictionary<string, bool> IsVariableObject;

    //[SerializeField]
    //private List<string> keys;
    //[SerializeField]
    //private List<object> values;

    public AI_Blackboard()
    {
        ObjectPool = new Dictionary<string,object>();
        IsVariableObject = new Dictionary<string, bool>();
    }

    // Default getObject (runs in trouble when it doesnt exist)
    public object this[string name]
    {
        get { return GetObject(name); }
        set { SetObject(name, value); }
    }

    #region Get

    public T GetObject<T>(string name, bool createIfNonexistant = true)
    {
        // Early exit if non-existant
        if (!ObjectPool.ContainsKey(name))
        {
            if (createIfNonexistant)
                ObjectPool[name] = default(T);
            else
                return DoesNotContainKey<T>(name);
        }
        return (T)ObjectPool[name];
    }

    public object GetObject(string name)
    {
        if (!ObjectPool.ContainsKey(name))
            return DoesNotContainKey<object>(name);

        return ObjectPool[name];
       // return GetObject<object>(name, createIfNonexistant);
    }

    public object GetObjectOrSetDefault(string name, object newDefault)
    {
        if (ObjectPool.ContainsKey(name))
            return ObjectPool[name];

        SetObject(name,newDefault);
        return newDefault;
    }

    #endregion

    private T DoesNotContainKey<T>(string name)
    {
        Debug.Log("AI_Blackboard.GetObject(" + name + ") does not exist in dictionary. Creating default.");
        return default(T);
    }

    /// <summary>
    /// Null makes it a variable type in the editorInspector
    /// </summary>
    public void SetObject(string name, object obj)
    {
         // Null makes it a variable type in the editorInspector
        if (!IsVariableObject.ContainsKey(name))
        {
            if (obj == null)
                IsVariableObject[name] = true;
            else
                IsVariableObject[name] = false;
        }

        // Set the object
        ObjectPool[name] = obj;
    }
}
