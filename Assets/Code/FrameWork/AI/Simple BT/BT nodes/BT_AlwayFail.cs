﻿using UnityEngine;
using System.Linq;
using System.Collections;

public class BT_AlwayFail : BT_Decorator 
{
    public BT_AlwayFail() : base() 
    {
        Description.Name = "AlwayFail";
        Description.Description = "Fires up child and then always returns failed";
    }

    protected override BT_Behavior.Status update()
    {
        // Fire off child
        if(Node.HasChildren)
            Node.Children.First().Tick(Agent);

        return Status.Failed;//exit(agent, Status.Failed);
    }
}
