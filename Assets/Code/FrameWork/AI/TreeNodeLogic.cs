﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Can only have one parent,
/// Childrens parent all need to be the parent
/// </summary>
/// <typeparam name="T"></typeparam>
public class TreeNodeLogic<T>: EasyScriptableObject<T> where T : TreeNodeLogic<T>
{
    public T Parent;
    public List<T> Children;

    public TreeNodeLogic<T> This { get { return this; } }

    public void AddChildren(params T[] children)
    {
        foreach (T newChild in children)
        {
            // Check if the child already has a parent
            if (newChild.Parent != null)
            {
                // Remove new child from old parent
                newChild.Parent.RemoveChildren(newChild);
            }

            // Check if the parent is this node's parent
            if (Parent == newChild)
            {
                // Remove this child from the ex parent
                Parent.RemoveChildren((T)this);
            }

            // Set new parent
            newChild.Parent = (T)this;
        }

        Children.AddRange(children.ToList());
    }

    public void RemoveChildren(params T[] children)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].Parent = null;
            Children.Remove(children[i]);
        }
    }
}