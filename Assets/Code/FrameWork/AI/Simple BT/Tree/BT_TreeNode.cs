using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Status = BT_Behavior.Status;
using System;

[System.Serializable]
public class BT_TreeNode : TreeNodeLogic<BT_TreeNode>
{
    #region Fields

    [SerializeField,HideInInspector]
    private BT_BBParameters behavior;

    [SerializeField,ReadOnly]
    private string behaviorType;

    [SerializeField]
    private AI_Blackboard parametersBB;

    [SerializeField]
    private BT_Tree tree;

    #endregion

    #region Properties
    
    public BT_BBParameters Behavior
    {
        get 
        {
            if (behavior == null)
            {
                if (behaviorType == string.Empty)
                {
                    Debug.LogError("Behavior is null and cannot reacreate from type");
                    return null;
                }
                behavior = (BT_BBParameters)Activator.CreateInstance(Type.GetType(behaviorType));
            }
            return behavior; 
        }
        set { behaviorType = value.GetType().ToString(); SetParameters(value); }
    }

    public AI_Blackboard ParametersBB { get { return parametersBB; } private set { parametersBB = value; } }

    public BT_Tree Tree { get { return tree; } private set { tree = value; } }

    public bool HasChildren { get { return Children.Count > 0; } }

    public bool IsRoot { get { return Parent == null; } }

    public override int ID
    {
        get
        {
            return base.ID;
        }
        set
        {
            base.ID = value;
            SetNames();
        }
    }

    
    #endregion

    #region Constructor

    public override void Init(HideFlags newHideFlag = HideFlags.None)
    {
        base.Init(newHideFlag);

        // Default values
        ID = -1337;
        if (ParametersBB == null)
            ParametersBB = AI_Blackboard.Create();
        Parent = null;
        Children = new List<BT_TreeNode>();
        behavior = null;
    }

    public static BT_TreeNode CreateNode(BT_BBParameters behavior,int ID, AI_Blackboard bb = null, BT_Tree treeObj = null )
    {
        // Create node and set values
        BT_TreeNode node = Create();
        node.Behavior = behavior;
        node.ID = ID;
        node.Tree = treeObj;

        if (bb != null)
            node.ParametersBB = bb;

        // Add object to asset
        if(treeObj != null)
            node.AddObjectToAsset(treeObj);
            
        return node;
    }

    private void SetNames()
    {
        name = ID + " | TREENODE | " + (behavior!=null?behavior.Description.Name:"");
    }


    public static BT_TreeNode CreateNode(BT_BBParameters behavior, string filepath = "")
    {
        BT_TreeNode node = Create();
        node.Behavior = behavior;

        if (filepath != string.Empty)
            node.AddObjectToAsset(filepath);

        return node;
    }
    #endregion

    #region Set Parameters

    private void SetParameters(BT_BBParameters behavior)
    {
        // Set behavior
        this.behavior = behavior;

        //Debug.Log("SetParameters");

        // Reset blackboard
        ParametersBB.Clear();

        // Call the SetnodeParameters virtual method
        // Sets the blackboard with default parameters
        behavior.SetNodeParameters(this);
    }

    

    /// <summary>
    /// Returns false if the class already exists and true if it had to be created
    /// </summary>
    public bool CheckAndSetClass<T>() where T: BT_BBParameters
    {
        if (Behavior.GetType() == typeof(T))
            return false;

        // Not the same type so reset the behavior
        T newBehavior = (T)Activator.CreateInstance(typeof(T));
        Behavior = newBehavior;
        return true;
    }

    #endregion

    public Status Tick(AI_Agent agent)
    {
        return Behavior.Tick(agent, ID);
    }
}