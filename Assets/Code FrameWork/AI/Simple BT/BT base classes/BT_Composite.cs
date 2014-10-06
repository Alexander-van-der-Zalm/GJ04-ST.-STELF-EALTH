using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BT_Composite : BT_Behavior 
{
    protected List<BT_Behavior> Children;
    private string depth = "Depth";
    protected override Status update(AI_Agent agent)
    {
        if(agent!=null)
            agent[depth] = (int)agent[depth] + 1;

        Debug.Log(agent[depth]);

        return base.update(agent);
    }

    protected override void onTerminate(AI_Agent agent, Status status)
    {
        if (agent != null)
            agent[depth] = (int)agent[depth] - 1;
    }
}
