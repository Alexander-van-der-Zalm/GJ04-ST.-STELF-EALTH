using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AI_Blackboard 
{
    public string Name = "AI BlackBoard";



    public Dictionary<string, object> ObjectPool 
    { 
        get { return objectPool == null? objectPool = new Dictionary<string, object>() : objectPool; } 
        private set { objectPool = value; } 
    }
    private Dictionary<string, object> objectPool;

    public Dictionary<string, bool> IsVariableObject 
    { 
        get { return isVariableObject ==  null ? isVariableObject = new Dictionary<string,bool>() : isVariableObject; } 
        private set { isVariableObject = value; } 
    }
    private Dictionary<string, bool> isVariableObject;

    [SerializeField]
    private List<string> keys;
    [SerializeField]
    private List<object> objectsValues;
    [SerializeField]
    private List<bool> variableObjects;
    [SerializeField]
    private bool instantiated;
    [SerializeField]
    private bool prepared;
    
    #region Constructor

    //public AI_Blackboard()
    //{
        
    //}

    public void Clear()
    {
        Debug.Log("Clear Called");
        ObjectPool.Clear();
        IsVariableObject.Clear();
    }

    public void Init(bool overrideInit = false)
    {
        //Debug.Log("Init Called");
        if (instantiated || !overrideInit)
            return;

        ObjectPool = new Dictionary<string, object>();
        IsVariableObject = new Dictionary<string, bool>();
        instantiated = true;
        prepared = false;
        Debug.Log("Init Completed");
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

    //public T GetObject<T>(string name, bool createIfNonexistant = true)
    //{
    //    // Early exit if non-existant
    //    if (!ObjectPool.ContainsKey(name))
    //    {
    //        if (createIfNonexistant)
    //            ObjectPool[name] = default(T);
    //        else
    //            return DoesNotContainKey<T>(name);
    //    }
    //    return (T)ObjectPool[name];
    //}

    public object GetObject(string name)
    {
        Init();
        
        if (!ObjectPool.ContainsKey(name))
            return DoesNotContainKey<object>(name);

        return ObjectPool[name];
       // return GetObject<object>(name, createIfNonexistant);
    }

    public object GetObjectOrSetDefault(string name, object newDefault)
    {
        Init();
        
        if (ObjectPool.ContainsKey(name))
            return ObjectPool[name];

        SetObject(name,newDefault);
        return newDefault;
    }

    #endregion

    #region Helpers

    private T DoesNotContainKey<T>(string name)
    {
        Debug.Log("AI_Blackboard.GetObject(" + name + ") does not exist in dictionary. Creating and saving default.");
        return default(T);
    }

    #endregion

    #region Set

    /// <summary>
    /// Null makes it a variable type in the editorInspector
    /// </summary>
    public void SetObject(string name, object obj)
    {
        Init();

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

    public void ChangeValues(AI_Blackboard bb)
    {
        ObjectPool = bb.ObjectPool;
        IsVariableObject = bb.IsVariableObject;
    }

    #region Serialization Helpers

    public void PrepareSerialization()
    {
        keys = ObjectPool.Keys.ToList();
        objectsValues = ObjectPool.Values.ToList();
        variableObjects = IsVariableObject.Values.ToList();
        prepared = true;

        Debug.Log("Prepare keys: " + keys.Count + " objects: " + objectsValues.Count + " bools: " + variableObjects.Count);
    }

    public void Reconstruct()
    {
        Init();

        if (keys == null || objectsValues == null || variableObjects == null)
        {
            Debug.Log("No preperation has happened");
            return;
        }
        Debug.Log("Reconstruct keys:" + prepared);
        Debug.Log("Reconstruct keys:" + keys.Count + " objects: " + objectsValues.Count + " bools: " + variableObjects.Count);
        
        for (int i = 0; i < keys.Count;i++)
        {
            string key = keys[i];
            object obj = objectsValues[i];
            bool isVar = variableObjects[i];
            ObjectPool[key] = obj;
            IsVariableObject[key] = isVar;
        }
    }

    #endregion
}
