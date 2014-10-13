using UnityEngine;
using System.Collections;

public class BT_BehaviorDelegator : BT_Action 
{
    public delegate Status UpdateDelegate(AI_Agent agent, NodeDescription description);
    public delegate void InitDelegate(AI_Agent agent, NodeDescription description);
    public delegate void TerminateDelegate(AI_Agent agent, NodeDescription description, Status status);

    private InitDelegate init;
    private TerminateDelegate terminate;
    private UpdateDelegate updatedel;

    public BT_BehaviorDelegator(NodeDescription.BT_NodeType type, UpdateDelegate update, InitDelegate onInit = null, TerminateDelegate onTerm = null)
    {
        Description.Type = type;
        init = onInit;
        this.updatedel = update;
        terminate = onTerm;
    }

    protected override Status update(AI_Agent agent)
    {
        return updatedel(agent, Description);
    }

    protected override void onInitialize(AI_Agent agent)
    {
        if(init!=null)
            init(agent, Description);
    }

    protected override void onTerminate(AI_Agent agent, Status status)
    {
        if(terminate!=null)
            terminate(agent, Description, status);
    }
}
