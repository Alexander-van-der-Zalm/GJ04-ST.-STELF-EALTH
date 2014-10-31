using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScriptableObjectCollection<T> : EasyScriptableObject<ScriptableObjectCollection<T>>, IList<T> where T : IInitSO, IEasyScriptableObject
{
    [SerializeField]
    private List<T> soCollection;

    public List<T> Collection
    {
        get { return soCollection; }// == null ? soCollection = new List<T>() : soCollection; }
        private set { soCollection = value; }
    }

    public override void Init(HideFlags newHideFlag = HideFlags.None)
    {
        hideFlags = newHideFlag;
        soCollection = new List<T>();
    }

    #region List Implementation

    public void Add(T newSO)
    {
        // Add to list
        Collection.Add(newSO);
    }

    public int IndexOf(T item)
    {
        return Collection.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        Collection.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Collection.RemoveAt(index);
    }

   

    public T this[int index]
    {
        get
        {
            return Collection[index];
        }
        set
        {
            Collection[index] = value;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return Collection.GetEnumerator();
    }


    public void Clear()
    {
        Collection.Clear();
    }

    public bool Contains(T item)
    {
        return Collection.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        Collection.CopyTo(array, arrayIndex);
    }

    public int Count
    {
        get { return Collection.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool Remove(T item)
    {
        return Collection.Remove(item);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return Collection.GetEnumerator();
    }

    #endregion
}
