using UnityEngine;
using System.Collections;
using UnityEditor;

public class EasyScriptableObject<T> : ScriptableObject,IEasyScriptableObject where T : ScriptableObject, IEasyScriptableObject
{
    public static T Create()
    {
        T obj = ScriptableObject.CreateInstance<T>();
        obj.Init();
        return obj;
    }

    public static T CreateObjAndAsset(string path, HideFlags newHideFlag = HideFlags.None)
    {
        T obj = Create();

        obj.CreateAsset(path);

        return obj;
    }

    public static T CreateObjAddToAsset(string path, HideFlags newHideFlag = HideFlags.None)
    {
        T obj = Create();

        obj.AddObjectToAsset(path);
        
        return obj;
    }
    public void CreateAsset(string path)
    {
        AssetDatabase.CreateAsset(this, path);
        AssetDatabase.ImportAsset(path);
    }

    public void AddObjectToAsset(UnityEngine.Object obj)
    {
        // Fail if the object is not an asset
        if (!AssetDatabase.Contains(obj))
        {
            Debug.LogError("EasyScriptableObject.AddObjectToAsset(Object) object " + obj.name + " " + obj.GetType() + " is not an asset");
        }

        string path = AssetDatabase.GetAssetPath(obj);

        // Add the object
        AssetDatabase.AddObjectToAsset(this, obj);

        // Refresh/Reimport
        AssetDatabase.Refresh();
        AssetDatabase.ImportAsset(path);
    }

    public void AddObjectToAsset(string path)
    {
        AssetDatabase.AddObjectToAsset(this, path);
        AssetDatabase.ImportAsset(path);
    }


    public virtual void Init(HideFlags newHideFlag = HideFlags.None)
    {
        hideFlags = newHideFlag;
    }
}

public interface IEasyScriptableObject
{
    void Init(HideFlags newHideFlag = HideFlags.None);
    void CreateAsset(string path);
    void AddObjectToAsset(string path);
    void AddObjectToAsset(UnityEngine.Object obj);
}
