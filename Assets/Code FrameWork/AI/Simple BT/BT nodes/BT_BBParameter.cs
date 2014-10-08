using UnityEngine;
using System.Collections;

public class BT_BBParameter : BT_Behavior 
{
    protected string parameter1 = "Parameter1";
    protected string parameter1bb = "Parameter1BB";

    public BT_BBParameter(string bbParameter, AI_Agent.BlackBoard accesparam1):base()
    {
        Description.NodeBlackBoard[parameter1] = bbParameter;
        Description.NodeBlackBoard[parameter1bb] = accesparam1;
    }

    protected object GetObject1(AI_Agent agent)
    {
        return GetObject(agent, parameter1, parameter1bb, false);
    }

    protected void SetObject1(AI_Agent agent, object obj)
    {
        SetObject(agent, parameter1, parameter1bb, obj);
    }

    protected object GetObject(AI_Agent agent, string nodeBBParameter, string localOrGlobalBBnodeParamter, bool isObject)
    {
        // If the object is stored directly in the node blackboard, return it
        if(isObject)
            return Description.NodeBlackBoard[nodeBBParameter];
        
        // Else get the proper objects from the agents local or global blackboard
        AI_Agent.BlackBoard acces = (AI_Agent.BlackBoard)Description.NodeBlackBoard[localOrGlobalBBnodeParamter];
        
        string accesParam = (string)Description.NodeBlackBoard[nodeBBParameter];
        switch(acces)
        {
            case AI_Agent.BlackBoard.global:
                return agent.GlobalBlackboard[accesParam];
            case AI_Agent.BlackBoard.local:
                return agent.LocalBlackboard[accesParam];
        }

        // Should never happen
        Debug.Log("BT_CheckEqualBBParameter.GetObject has encountered an error");
        return null;
    }

    protected void SetObject(AI_Agent agent, string nodeBBParameter, string localOrGlobalBBnodeParamter, object obj)
    {
        // Set the objects from the agents local or global blackboard
        AI_Agent.BlackBoard acces = (AI_Agent.BlackBoard)Description.NodeBlackBoard[localOrGlobalBBnodeParamter];

        string accesParam = (string)Description.NodeBlackBoard[nodeBBParameter];
        switch (acces)
        {
            case AI_Agent.BlackBoard.global:
                agent.GlobalBlackboard[accesParam] = obj;
                break;
            case AI_Agent.BlackBoard.local:
                agent.LocalBlackboard[accesParam] = obj;
                break;
        }
    }
}
