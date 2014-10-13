using UnityEngine;
using System.Collections;

public class BT_BBParameters : BT_Behavior 
{
    /// <summary>
    /// Get objects from the node's memory (shared across agents)
    /// </summary>
    /// <param name="name">dictionary index</param>
    /// <returns>object</returns>
    public object this[string name]
    {
        get { return Description.NodeBlackBoard[name]; }
        set { Description.NodeBlackBoard[name] = value; }
    }

    public AI_AgentBBAccessParameter Par(string name)
    {
        return (AI_AgentBBAccessParameter)Description.NodeBlackBoard[name];
    }

    public object GetAgentObject(AI_AgentBBAccessParameter a, AI_Agent agent)
    {
        if (a.AgentAccesType == AI_Agent.BlackBoard.local)
            return agent.LocalBlackboard[a.ParameterName];
        else
            return agent.GlobalBlackboard[a.ParameterName];
    }

    public void SetAgentObject(AI_AgentBBAccessParameter a, AI_Agent agent, object obj)
    {
        if (a.AgentAccesType == AI_Agent.BlackBoard.local)
            agent.LocalBlackboard[a.ParameterName] = obj;
        else
            agent.GlobalBlackboard[a.ParameterName] = obj;
    }
}
