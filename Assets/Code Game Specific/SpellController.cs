using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpellOrb;
    public Stats Stats;

    private float radius;
    private Transform tr;
    private Vector3 newDir;
    private float rotation;

	// Use this for initialization
	void Start () 
    {
        tr = transform;
        Vector3 offset = SpellOrb.position - tr.position;
        Vector2 xyplane = new Vector2(offset.x, offset.z);
        radius = xyplane.magnitude;
        newDir = new Vector3(0, 0, 1)*radius;
	}
	
	// Update is called once per frame
	void Update () 
    {
        updateOrbLocation();
	}

    private void updateOrbLocation()
    {
        SpellOrb.position = newDir + tr.position;
        SpellOrb.localRotation = Quaternion.AngleAxis(rotation,new Vector3(0,1,0));
    }

    public void MoveOrb(Vector2 dir)
    {
        if (dir == Vector2.zero)
            return;

        newDir = dir.normalized * radius;
        newDir.z = newDir.y;
        newDir.y = 0;
        rotation = Mathf.Atan2(dir.x, dir.y)/Mathf.PI * 180;
    }

    public void Launch(SpellBase spell, Transform caster, Action action)
    {
        //Debug.Log("Launch");
        spell.Activate(SpellOrb, caster,Stats, action);
    }
}
