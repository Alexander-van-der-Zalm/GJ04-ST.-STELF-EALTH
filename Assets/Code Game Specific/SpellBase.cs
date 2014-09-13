using UnityEngine;
using System.Collections;

public abstract class SpellBase:MonoBehaviour 
{
    public abstract void Activate(Transform SpellOrb, Transform Caster, Stats casterStats, Action action);
}
