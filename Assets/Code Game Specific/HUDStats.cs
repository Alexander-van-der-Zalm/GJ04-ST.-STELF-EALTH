using UnityEngine;
using System.Collections;

public class HUDStats : MonoBehaviour 
{
    public GUI_Bar Shield, Hull, Energy;
    public Stats Stats;

	// Use this for initialization
	void Start () 
    {  
        Shield.MaxValue = Stats.ShieldMax;
        Hull.MaxValue = Stats.HullMax;
        Energy.MaxValue = Stats.EnergyMax;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Shield.Value = Stats.Shield;
        Hull.Value = Stats.Hull;
        Energy.Value = Stats.Energy;
	}
}
