using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
    public float ShieldMax;
    public float ShieldPerDodge;

    public float HullMax;

    public float EnergyMax;
    public float EnergyRegenPerSecond;
    public float EnergyEmptyCooldown;

    public bool EnergyRegenPause = false;
    public bool EnergyOverdrawn { get { return EnergyCooldown; } }
    private bool EnergyCooldown;

    public float Shield, Hull, Energy;
    
	// Use this for initialization
	void Start () 
    {
        Shield = ShieldMax;
        Hull = HullMax;
        Energy = EnergyMax;
	}
	
	// Update is called once per frame
	void Update () 
    {
        maintainEnergy();
	}

    private void maintainEnergy()
    {
        if (EnergyCooldown || EnergyRegenPause)
            return;

        Energy = Mathf.Min(EnergyMax, Energy + EnergyRegenPerSecond * Time.deltaTime);
    }

    public void DoDamage(float damage, float shieldBonus = 0, float hullBonus = 0, float energyBonus = 0, float energyEmptyDamage = 0)
    {
        // Shield portion of the damage
        if (Shield > 0)
        {
            Shield -= damage;
            if (Shield < 0)
                damage = shieldBroken();
            else
            {
                damage = 0;
                Shield -= shieldBonus;
                if (Shield < 0)
                    shieldBroken();
            }
        }

        // Hull damage
        if (damage > 0)
        {
            Hull -= (damage + hullBonus);
            if (Hull < 0)
            {
                destroy();
                return;
            }
        }

        if (energyBonus > 0 && ConsumeEnergy(energyBonus) > 0.0f)
        {
            DoDamage(energyEmptyDamage);
            //energyEmptyPunishAnimation();
        }
    }

    private void energyEmptyPunishAnimation()
    {
        throw new System.NotImplementedException();
    }

    private void destroy()
    {
        throw new System.NotImplementedException();
    }

    private float shieldBroken()
    {
        // Shield Broken animation
        float result = Shield;
        Shield = 0;
        return result;
    }

    public float ConsumeEnergy(float energyCost)
    {
        //Debug.Log(energyCost);
        float defecit = 0;
        Energy -= energyCost;

        if (Energy < 0)
        {
            defecit = Energy;
            Energy = 0;
            StartCoroutine(StartEnergyEmptyCooldown());
        }

        return defecit;
    }

    private IEnumerator StartEnergyEmptyCooldown()
    {
        EnergyCooldown = true;

        yield return new WaitForSeconds(EnergyEmptyCooldown);

        EnergyCooldown = false;
    }

}
