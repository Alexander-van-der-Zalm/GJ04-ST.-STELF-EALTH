using UnityEngine;
using System.Collections;

public class AimingEnergyOrbs : MonoBehaviour 
{
    public Stats PlayerShipStats;
    public Color FullEnergyColor, LowEnergyColor, OverdrawnfEnergyColor;

    private Material sharedMaterial;

	// Use this for initialization
	void Start () 
    {
        sharedMaterial = renderer.sharedMaterial;

	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(PlayerShipStats.EnergyOverdrawn)
        {
            sharedMaterial.color = OverdrawnfEnergyColor;
            return;
        }
        float e = PlayerShipStats.Energy / PlayerShipStats.EnergyMax;
        Color lerped = Color.Lerp(LowEnergyColor,FullEnergyColor, e);

        sharedMaterial.color = lerped;
	}
}
