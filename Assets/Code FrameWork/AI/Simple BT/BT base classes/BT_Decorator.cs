using UnityEngine;
using System.Collections;

public class BT_Decorator : BT_Behavior 
{
    public BT_Decorator(AI_Agent agent)
        : base(agent)
    {
        Description.Type = NodeDescription.BT_NodeType.Decorator;
    } 
}
