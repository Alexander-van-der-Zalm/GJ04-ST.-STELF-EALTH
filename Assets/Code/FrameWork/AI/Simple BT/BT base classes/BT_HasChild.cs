using UnityEngine;
using System.Collections;

public class BT_HasChild : BT_BBParameters
{
    protected string depth = "Depth";

    protected override Status update(AI_Agent agent, int id)
    {
        if (agent != null)
            agent[depth] = (int)agent.GSD(depth, 0) + 1;

        //Debug.Log("down: " + agent[depth]);
        return base.update(agent, id);
    }

    protected Status exit(AI_Agent agent, Status status)
    {
        if (agent != null)
            agent[depth] = (int)agent[depth] - 1;

        //Debug.Log("up: " + agent[depth]);
        return status;
    }
}
