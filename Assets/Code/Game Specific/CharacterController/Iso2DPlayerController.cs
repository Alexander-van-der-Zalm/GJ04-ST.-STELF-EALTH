using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Iso2DCharacterController))]
public class Iso2DPlayerController : MonoBehaviour 
{
    public enum PlayerActions
    {
        PickPocket = 0,
        Dance = 1
    }

    public ControlScheme ControlScheme;

    private Iso2DCharacterController cc;

	// Use this for initialization
	void Start ()
    {
        cc = gameObject.GetComponent<Iso2DCharacterController>();

        if (ControlScheme == null)
        {
            ControlScheme = ControlScheme.CreateScheme<PlayerActions>();
            ControlScheme.Actions[(int)PlayerActions.PickPocket].Keys.Add(ControlKey.PCKey(KeyCode.Space));
            ControlScheme.Actions[(int)PlayerActions.PickPocket].Keys.Add(ControlKey.XboxButton(XboxCtrlrInput.XboxButton.A));
            ControlScheme.Actions[(int)PlayerActions.Dance].Keys.Add(ControlKey.PCKey(KeyCode.LeftShift));
            ControlScheme.Actions[(int)PlayerActions.Dance].Keys.Add(ControlKey.XboxButton(XboxCtrlrInput.XboxButton.B));
            ScriptableObjectHelper.SaveAssetAutoNaming(ControlScheme);
            //ControlScheme.hideFlags = HideFlags.DontSave;

        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        ControlScheme.Update();

        //cc.SetInput(ControlScheme.Horizontal.Value(), ControlScheme.Vertical.Value());

        if(ControlScheme.Actions[(int)PlayerActions.Dance].IsPressed())
        {
            cc.Dance();
        }
	}
}
