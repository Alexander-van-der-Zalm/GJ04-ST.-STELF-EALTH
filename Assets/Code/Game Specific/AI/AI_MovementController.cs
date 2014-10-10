using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class AI_MovementController : MonoBehaviour 
{
    private Transform tr;
    private CharacterController cc;

    
    public bool ClickToOrder = false;
    public Vector2 Dest = new Vector2();
    [ReadOnly]
    public float Distance = 0;

    //private Coroutine order;
    public float StoppingRange = 0.5f;

	// Use this for initialization
	void Start () 
    {
        cc = GetComponent<CharacterController>();
        tr = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (ClickToOrder)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //Stop();
                Dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Start(Dest);
            }
        }
            
	}

    public void Start(Vector2 destination)//, float stoppingrange = 0)
    {
        Stop();
        StartCoroutine(OrderMoveToCR(destination));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator OrderMoveToCR(Vector2 destination)
    {
        Distance = getDistance(destination);

        while (Distance > StoppingRange)
        {
            moveTo(destination);
            Distance = getDistance(destination);
            yield return null;
        }

        cc.SetInput(0, 0);
    }

    private float getDistance(Vector2 destination)
    {
        Vector2 pos = new Vector2(tr.position.x, tr.position.y);
        return (destination - pos).magnitude;
    }

    private void moveTo(Vector2 destination)
    {
        Vector2 pos = new Vector2(tr.position.x,tr.position.y);
        Vector2 dir = destination - pos;
        dir.Normalize();
        cc.SetInput(dir.x, dir.y);
    }
}
