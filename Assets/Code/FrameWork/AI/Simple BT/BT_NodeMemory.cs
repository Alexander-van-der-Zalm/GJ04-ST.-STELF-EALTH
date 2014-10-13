using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BT_NodeMemory
{
    public List<BT_NodeMemory> Children;
    //public Status Status;
    public int ID;
    public BT_Behavior Node;

    BT_NodeMemory(int id, BT_Behavior behaviour, params BT_NodeMemory[] childrenMem)
    {
        //this.Status = Status.Invalid;
        Node = behaviour;
        ID = id;
        Children = childrenIDs.ToList();
    }
}