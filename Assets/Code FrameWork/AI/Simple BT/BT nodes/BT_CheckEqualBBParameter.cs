using UnityEngine;
using System.Collections;

public class BT_CheckEqualBBParameter : BT_Condition 
{
    string parameter1 = "Parameter1";
    string parameter2 = "Parameter2";
    string parameter1bb = "Parameter1BB";
    string parameter2bb = "Parameter2BB";
    bool param2IsObject = false;

    public BT_CheckEqualBBParameter(string bbParameter, object equalObject, AI_Agent.BlackBoard param1):base()
    {
        Description.NodeBlackBoard[parameter1] = bbParameter;
        Description.NodeBlackBoard[parameter2] = equalObject;

        Description.NodeBlackBoard[parameter1bb] = param1;
        Description.NodeBlackBoard[parameter2bb] = null;
    }

    public BT_CheckEqualBBParameter(string bbParameter1, string bbParameter2, AI_Agent.BlackBoard param1, AI_Agent.BlackBoard param2)
        : base()
    {
        Description.NodeBlackBoard[parameter1] = bbParameter1;
        Description.NodeBlackBoard[parameter2] = bbParameter2;

        Description.NodeBlackBoard[parameter1bb] = param1;
        Description.NodeBlackBoard[parameter2bb] = param2;
    }

    protected override Status update(AI_Agent agent)
    {
        object obj1 = GetObject(agent,parameter1,parameter1bb,false);
        object obj2 = GetObject(agent,parameter2,parameter2bb,false);

        // If equals, then succes, else failed
        return obj1.Equals(obj2) ? Status.Succes : Status.Failed;
    }

    private object GetObject(AI_Agent agent, string nodeBBParameter, string localOrGlobalBBnodeParamter, bool isObject)
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
}
