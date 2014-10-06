using UnityEngine;
using System.Collections;

public class BT_Action : BT_Behavior 
{
    public override void Instantiate()
    {
        Description.Type = NodeDescription.BT_NodeType.Action;
    }
}
