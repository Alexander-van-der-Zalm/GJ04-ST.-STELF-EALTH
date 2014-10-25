using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AI_Blackboard : EasyScriptableObject<AI_Blackboard>,ISerializationCallbackReceiver
{
    #region Fields

    public string Name = "AI BlackBoard";

    // Actual dictionaries dont need serialization
    private Dictionary<string, object> objectPool;
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

    #endregion

    #region Properties

    public Dictionary<string, object> ObjectPool 
    { 
        get { return objectPool == null? objectPool = new Dictionary<string, object>() : objectPool; } 
        private set { objectPool = value; } 
    }
    

    public Dictionary<string, bool> IsVariableObject 
    { 
        get { return isVariableObject ==  null ? isVariableObject = new Dictionary<string,bool>() : isVariableObject; } 
        private set { isVariableObject = value; } 
    }

    #endregion

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

    public override void Init(HideFlags newHideFlag = HideFlags.None)
    {
        // Exit out if already instantiated
        if (objectPool != null && IsVariableObject != null)
                return;
        
        base.Init(newHideFlag);

        //ObjectPool = new Dictionary<string, object>();
        //IsVariableObject = new Dictionary<string, bool>();

        SetObject("testvalue 1", 1);
        SetObject("TestParam", new AI_AgentBBAccessParameter());
        SetObject("TestVariableObject", null);

        // Temp
        //Debug.Log("Init COMPLETE");
    } 

    //public override void Init(HideFlags newHideFlag = HideFlags.None)
    //{
    //    //Debug.Log("Init Called");
    //    //if (instantiated)// || !overrideInit)
    //    //    return;

    //    Debug.Log("Init called");

    //    if (objectPool != null && IsVariableObject != null)
    //        return;

    //    base.Init(newHideFlag);


    //    ObjectPool = new Dictionary<string, object>();
    //    IsVariableObject = new Dictionary<string, bool>();
    //    //instantiated = true;
    //    //prepared = false;
    //    Debug.Log("Init Completed");
    //}

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
        //Init();
        
        if (!ObjectPool.ContainsKey(name))
            return DoesNotContainKey<object>(name);

        return ObjectPool[name];
       // return GetObject<object>(name, createIfNonexistant);
    }

    public object GetObjectOrSetDefault(string name, object newDefault)
    {
        //Init();
        
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
        //Init();

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

    public void OnBeforeSerialize()
    {
        keys = ObjectPool.Keys.ToList();
        objectsValues = ObjectPool.Values.ToList();
        variableObjects = IsVariableObject.Values.ToList();
        //prepared = true;
        //Debug.Log("OnBeforeSerialize complete");
        //Debug.Log("Prepare keys: " + keys.Count + " objects: " + objectsValues.Count + " bools: " + variableObjects.Count);
    }

    public void OnAfterDeserialize()
    {
        //Init();
        //Debug.Log("OnAfterDeserialize ");
        
        if (keys == null || objectsValues == null || variableObjects == null)
        {
            //Debug.Log("No preperation has happened");
            return;
        }
       // Debug.Log("Reconstruct keys:" + prepared);
        //Debug.Log("Reconstruct keys:" + keys.Count + " objects: " + objectsValues.Count + " bools: " + variableObjects.Count);
        
        for (int i = 0; i < keys.Count;i++)
        {
            string key = keys[i];
            object obj = objectsValues[i];
            bool isVar = variableObjects[i];
            ObjectPool[key] = obj;
            IsVariableObject[key] = isVar;
        }

        Debug.Log("OnAfterDeserialize complete");
    }

    #endregion
}
