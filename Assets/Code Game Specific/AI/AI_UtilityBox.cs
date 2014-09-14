using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AI_UtilityBox //: MonoBehaviour
{
    public AI_MovementController Controller;
    //public Transform Target;

    public AI_LineOfSightTriangle Los;
    public IAlert Alert;
}
