using UnityEngine;
using System.Collections;

public class BT_Action : BT_Behavior 
{
    public BT_Action(AI_Agent agent)
        : base(agent)
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }
}
