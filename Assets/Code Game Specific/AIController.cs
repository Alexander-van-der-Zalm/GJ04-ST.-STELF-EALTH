using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class AIController : MonoBehaviour 
{
    Transform tr;
    CharacterController cc;

    public bool MOVE = false;
    public Vector2 Dest = new Vector2();

	// Use this for initialization
	void Start () 
    {
        cc = GetComponent<CharacterController>();
        tr = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(MOVE)
            moveTo(Dest);
	}

    public void OrderMoveTo(Vector2 destination)
    {

    }

    private void moveTo(Vector2 destination)
    {
        Vector2 pos = new Vector2(tr.position.x,tr.position.y);
        Vector2 dir = destination - pos;
        dir.Normalize();
        cc.SetInput(dir.x, dir.y);
    }
}
