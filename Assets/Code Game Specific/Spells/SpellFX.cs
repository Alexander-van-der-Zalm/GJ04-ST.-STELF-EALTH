using UnityEngine;
using System.Collections;

public class SpellFX : ManagedObject
{
    public bool CenterOnSpellOrb = true;

    public bool SpellStart = false;
    private Transform centerOfSpell;

    void Update()
    {
        if (SpellStart && CenterOnSpellOrb)
            transform.position = centerOfSpell.position;
    }

    public SpellFX StartSpell(Transform centerOfCast)
    {
        SpellFX fx = Create().GetComponent<SpellFX>();
        fx.centerOfSpell = centerOfCast;
        //this.gameObject.SetActive(true);
        fx.SpellStart = true;
        return this;
    }
}
