using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BT_Composite : BT_Behavior 
{
    protected List<BT_Behavior> Children;

    public BT_Composite(AI_Agent agent):base (agent)
    {

    }
}
