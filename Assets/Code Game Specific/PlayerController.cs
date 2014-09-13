using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using System.Collections.Generic;

[RequireComponent(typeof(DirectMovementPhysics),typeof(SpellController),typeof(Stats))]
public class PlayerController : MonoBehaviour 
{
    public List<SpellBase> Spells;

    private ControlScheme controlScheme;
    private DirectMovementPhysics movPhysics;
    private SpellController spellController;
    private Stats stats;

	// Use this for initialization
	void Start () 
    {
        movPhysics = this.GetComponent<DirectMovementPhysics>();
        spellController = this.GetComponent<SpellController>();
        stats = this.GetComponent<Stats>();

        controlScheme = ControlManager.GetControlScheme(1);
        //Spells = new List<ISpellBase>();
        //Actions = new List<Action>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector2 movInput = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX), XCI.GetAxis(XboxAxis.LeftStickY));
        Vector2 aimInput = new Vector2(XCI.GetAxis(XboxAxis.RightStickX), XCI.GetAxis(XboxAxis.RightStickY));
        
        movPhysics.Move(movInput);
        
        if(aimInput.magnitude>0)
            spellController.MoveOrb(aimInput);

        if (controlScheme.Actions[0].IsPressed())
            spellController.Launch(Spells[0], transform, controlScheme.Actions[0]);
	}
}
