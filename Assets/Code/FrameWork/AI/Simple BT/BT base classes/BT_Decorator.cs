using UnityEngine;
using System.Collections;

public class BT_Decorator : BT_Behavior 
{
    protected BT_Behavior child;

    #region Constructor

    public BT_Decorator()
    {
        setDescription();
    }

    public BT_Decorator(BT_Behavior child)
    {
        SetChild(child);
        setDescription();
    }
    private void setDescription()
    {
        Description.Type = NodeDescription.BT_NodeType.Decorator;
    }

    #endregion

    public void SetChild(BT_Behavior child)
    {
        this.child = child;
    }
}
