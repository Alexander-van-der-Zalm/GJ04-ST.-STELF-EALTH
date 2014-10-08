using UnityEngine;
using System.Collections;

public class BT_BB2Parameters : BT_BBParameter
{
    protected string parameter2 = "Parameter2";
    protected string parameter2bb = "Parameter2BB";
    protected bool param2IsObject = false;

    public BT_BB2Parameters(string bbParameter, AI_Agent.BlackBoard accesparam1, object equalObject)
        : base(bbParameter,accesparam1)
    {
        Description.NodeBlackBoard[parameter2] = equalObject;
        Description.NodeBlackBoard[parameter2bb] = null;
        param2IsObject = true;
    }

    public BT_BB2Parameters(string bbParameter1, AI_Agent.BlackBoard param1, string bbParameter2, AI_Agent.BlackBoard param2)
        : base(bbParameter1, param1)
    {
        Description.NodeBlackBoard[parameter2] = bbParameter2;
        Description.NodeBlackBoard[parameter2bb] = param2;
    }

    protected object GetObject2(AI_Agent agent)
    {
        return GetObject(agent, parameter2, parameter2bb, param2IsObject);
    }

    protected void SetObject2(AI_Agent agent, object obj)
    {
        if (param2IsObject)
            Debug.Log("BT_BB2Parameters.SetObject2 is trying to set an object but param2 is an object");
        SetObject(agent, parameter2, parameter2bb, obj);
    }
}
