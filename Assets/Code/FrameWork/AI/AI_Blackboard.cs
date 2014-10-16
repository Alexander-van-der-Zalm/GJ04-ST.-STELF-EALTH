using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AI_Blackboard 
{
    public string Name = "AI BlackBoard";
    [System.NonSerialized]
    public Dictionary<string, object> ObjectPool;
    [System.NonSerialized]
    public Dictionary<string, bool> IsVariableObject;

    [SerializeField]
    private List<string> keys;
    [SerializeField]
    private List<object> objects;
    [SerializeField]
    private List<bool> variableObjects;

    #region Constructor

    public AI_Blackboard()
    {
        ObjectPool = new Dictionary<string,object>();
        IsVariableObject = new Dictionary<string, bool>();
        Debug.Log("Constructor Called");
    }

    #endregion

    #region Property

    // Default getObject (runs in trouble when it doesnt exist)
    public object this[string name]
    {
        get { return GetObject(name); }
        set { SetObject(name, value); }
    }

    #endregion

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

    #region Helpers

    private T DoesNotContainKey<T>(string name)
    {
        Debug.Log("AI_Blackboard.GetObject(" + name + ") does not exist in dictionary. Creating default.");
        return default(T);
    }

    #endregion

    #region Set

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

    #endregion

    #region Serialization Helpers

    public void PrepareSerialization()
    {
        keys = ObjectPool.Keys.ToList();
        objects = ObjectPool.Values.ToList();
        variableObjects = IsVariableObject.Values.ToList();

        Debug.Log("Prepare keys: " + keys.Count + " objects: " + objects.Count + " bools: " + variableObjects.Count);
    }

    public void Reconstruct()
    {
        ObjectPool = new Dictionary<string, object>();
        IsVariableObject = new Dictionary<string, bool>();

        if (keys == null || keys.Count == 0)
        {
            Debug.Log("No preperation has happened");
            return;
        }

        Debug.Log("Reconstruct keys:" + keys.Count + " objects: " + objects.Count + " bools: " + variableObjects.Count);
        
        for (int i = 0; i < keys.Count;i++)
        {
            string key = keys[i];
            object obj = objects[i];
            bool isVar = variableObjects[i];
            ObjectPool[key] = obj;
            IsVariableObject[key] = isVar;
        }
    }

    #endregion
}
