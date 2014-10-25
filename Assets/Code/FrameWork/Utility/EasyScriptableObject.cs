using UnityEngine;
using System.Collections;

public class EasyScriptableObject<T> : ScriptableObject,IInit where T : ScriptableObject, IInit
{
    public static T Create()
    {
        T obj = ScriptableObject.CreateInstance<T>();
        obj.Init();
        return obj;
    }

    public virtual void Init(HideFlags newHideFlag = HideFlags.None)
    {
        hideFlags = newHideFlag;
    }
}

public interface IInit
{
    void Init(HideFlags newHideFlag = HideFlags.None);
}
