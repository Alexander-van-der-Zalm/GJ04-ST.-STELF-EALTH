using UnityEngine;
using System.Collections;

public class BT_BehaviorDelegator : BT_Behavior 
{
    public delegate Status UpdateDelegate(NodeDescription description);
    public delegate void InitDelegate(NodeDescription description);
    public delegate void TerminateDelegate(NodeDescription description, Status status);

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

    protected override Status update()
    {
        return updatedel(Description);
    }

    protected override void onInitialize()
    {
        if(init!=null)
            init(Description);
    }

    protected override void onTerminate(Status status)
    {
        if(terminate!=null)
            terminate(Description, status);
    }
}
