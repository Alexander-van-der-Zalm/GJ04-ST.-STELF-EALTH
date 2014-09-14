using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour 
{
    public bool AlertWaypoint = false;
    
    public Vector2 GetLocation()
    {
        return transform.position;
    }
}
