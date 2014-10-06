using UnityEngine;
using System.Collections;

public class BT_Condition : BT_Behavior 
{
    public BT_Condition(AI_Agent agent)
        : base(agent)
    {
        Description.Type = NodeDescription.BT_NodeType.Condition;

    }
}
