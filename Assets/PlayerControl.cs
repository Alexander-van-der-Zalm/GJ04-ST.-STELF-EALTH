using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour 
{
    public enum PlayerActions
    {
        PickPocket

    }
    
    public ControlScheme ControlScheme;


    private Vector2 Input;

    private CharacterController cc;
    
	// Use this for initialization
	void Start () 
    {
        cc = gameObject.GetComponent<CharacterController>();

        ControlScheme = ControlScheme.CreateScheme<PlayerActions>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        ControlScheme.Update();
        cc.SetInput(ControlScheme.Horizontal.Value(), ControlScheme.Vertical.Value());
        Input = new Vector2(ControlScheme.Horizontal.Value(), ControlScheme.Vertical.Value());
	}
}
