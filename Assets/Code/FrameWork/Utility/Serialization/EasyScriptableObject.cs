using UnityEngine;
using System.Collections;
using UnityEditor;

public class EasyScriptableObject<T> : ScriptableObject, IEasyScriptableObject, IInitSO where T : ScriptableObject, IEasyScriptableObject, IInitSO
{
    public static T Create()
    {
        T obj = ScriptableObject.CreateInstance<T>();
        obj.Init(HideFlags.DontSave);
        return obj;
    }

    public static T CreateObjAndAsset(string path, HideFlags newHideFlag = HideFlags.DontSave)
    {
        T obj = Create();

        obj.CreateAsset(path);

        return obj;
    }

    public static T CreateObjAddToAsset(string path, HideFlags newHideFlag = HideFlags.DontSave)
    {
        T obj = Create();

        obj.AddObjectToAsset(path);
        
        return obj;
    }

    public void CreateAsset(string path)
    {
        //correctHideFlagsForSaving();
        if (hideFlags == HideFlags.DontSave)
            hideFlags = HideFlags.None;

        if (hideFlags == HideFlags.HideAndDontSave)
            hideFlags = HideFlags.HideInHierarchy;
        
        AssetDatabase.CreateAsset(this, path);
        AssetDatabase.ImportAsset(path);
        AssetDatabase.SaveAssets();
    }

    private void correctHideFlagsForSaving()
    {
        if (hideFlags == HideFlags.DontSave)
            hideFlags = HideFlags.None;

        if (hideFlags == HideFlags.HideAndDontSave)
            hideFlags = HideFlags.HideInHierarchy;
    }

    public void AddObjectToAsset(UnityEngine.Object obj)
    {
        // Fail if the object is not an asset
        if (!AssetDatabase.Contains(obj))
        {
            Debug.LogError("EasyScriptableObject.AddObjectToAsset(Object) object " + obj.name + " " + obj.GetType() + " is not an asset");
        }

        string path = AssetDatabase.GetAssetPath(obj);

        //correctHideFlagsForSaving();
        if (hideFlags == HideFlags.DontSave)
            hideFlags = HideFlags.None;

        if (hideFlags == HideFlags.HideAndDontSave)
            hideFlags = HideFlags.HideInHierarchy;

        // Add the object
        AssetDatabase.AddObjectToAsset(this, obj);

        // Refresh/Reimport
        AssetDatabase.Refresh();
        AssetDatabase.ImportAsset(path);
        AssetDatabase.SaveAssets();
    }

    public void AddObjectToAsset(string path)
    {
        AssetDatabase.AddObjectToAsset(this, path);
        AssetDatabase.ImportAsset(path);
    }

    public void RefreshAsset()
    {
        // TODO checks
        //string path = AssetDatabase.GetAssetPath(this);
        //Debug.Log(AssetDatabase.Contains(this) + " " + AssetDatabase.IsSubAsset(this));

        string path = AssetDatabase.GetAssetPath(this);
        if (AssetDatabase.IsSubAsset(this))
           path = AssetDatabase.GetAssetPath(AssetDatabase.LoadMainAssetAtPath(path));

        AssetDatabase.ImportAsset(path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        //if (AssetDatabase.Contains(this) && AssetDatabase.IsMainAsset(this))
        //    AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
        //else
        //{
            
        //    Debug.Log("RefreshAsset: Figure out how to refresh sub asset");
        //}
        
    }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(this);
    }

    public void DestroyImmediate()
    {
        UnityEngine.Object.DestroyImmediate(this, true);
    }

    public static V Create<V>() where V : ScriptableObject, IInitSO
    {
        V so = ScriptableObject.CreateInstance<V>();
        so.Init();
        return so;
    }

    public virtual void Init(HideFlags newHideFlag = HideFlags.None)
    {
        hideFlags = newHideFlag;
    }
}

public interface IEasyScriptableObject
{
    void CreateAsset(string path);
    void AddObjectToAsset(string path);
    void AddObjectToAsset(UnityEngine.Object obj);
    void Destroy();
    void DestroyImmediate();
}

public interface IInitSO
{
    void Init(HideFlags newHideFlag = HideFlags.None);
}
