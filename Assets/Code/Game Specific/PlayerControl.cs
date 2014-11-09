using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour 
{
    public enum PlayerActions
    {
        PickPocket = 0,
        Dance = 1
    }
    
    public ControlScheme ControlScheme;
    
    [SerializeField,HideInInspector]
    private CharacterController cc;


    private PickPocket pp;

	// Use this for initialization
	void Start () 
    {
        cc = gameObject.GetComponent<CharacterController>();
        pp = gameObject.GetComponent<PickPocket>();
       
        if(ControlScheme == null)
        {
            ControlScheme = ControlScheme.CreateScheme<PlayerActions>();
            ControlScheme.Actions[(int)PlayerActions.PickPocket].Keys.Add(ControlKey.PCKey(KeyCode.Space));
            ControlScheme.Actions[(int)PlayerActions.PickPocket].Keys.Add(ControlKey.XboxButton(XboxCtrlrInput.XboxButton.A));

            ScriptableObjectHelper.SaveAssetAutoNaming(ControlScheme);
            //ControlScheme.hideFlags = HideFlags.DontSave;

        }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        ControlScheme.Update();

        cc.SetInput(ControlScheme.Horizontal.Value(), ControlScheme.Vertical.Value());
        pp.SetInput(ControlScheme.Actions[(int)PlayerActions.PickPocket].IsPressed());
	}
}
