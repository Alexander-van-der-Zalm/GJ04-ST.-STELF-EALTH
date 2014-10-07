using UnityEngine;
using System.Collections;

public class BT_Decorator : BT_Behavior 
{
    protected BT_Behavior child;

    public BT_Decorator(BT_Behavior child)
    {
        this.child = child;
        Description.Type = NodeDescription.BT_NodeType.Decorator;
    } 
}
