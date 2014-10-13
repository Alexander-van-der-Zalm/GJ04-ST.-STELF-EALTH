using UnityEngine;
using System.Collections;

[System.Serializable]
public class AI_AgentBBAccessParameter
{
    public AI_Agent.BlackBoard AgentAccesType;
    public string ParameterName;

    public GUIContent Content;

    public AI_AgentBBAccessParameter(string parametername, AI_Agent.BlackBoard access, GUIContent content)
    {
        Content = content;
        ParameterName = parametername;
        content = Content;
    }

    public AI_AgentBBAccessParameter(string parametername = "", AI_Agent.BlackBoard access = AI_Agent.BlackBoard.local)
        : this(parametername, access, null)
    { }
}
