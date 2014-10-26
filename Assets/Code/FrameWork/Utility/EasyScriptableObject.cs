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

}
