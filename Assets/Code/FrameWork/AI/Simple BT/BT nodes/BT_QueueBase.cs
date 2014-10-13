using UnityEngine;
using System.Linq;
using System.Collections;
using Framework.Collections;

public class BT_QueueBase : BT_Action 
{
    protected const string Queue = "QueueParameter";

    protected bool HasIQueue(object obj)
    {
        bool has = obj.GetType().GetInterfaces().Contains(typeof(IQueue));
        if(!has)
            Debug.Log("BT_QueuePush has invalid object behind paramters, needs: " + typeof(Queue<>));
        return has;
    }
}
