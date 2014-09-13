using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GrowingBall : SpellBase
{
    #region Classes

    [HideInInspector]
    //[System.Serializable]
    public class AnimationInfo
    {
        public enum AnimState
        {
            Starting, Overcharging, Cooldown
        }
        public AnimState State;
        public float AnimStateProgress;

        public bool Activated = false;
        public bool InputReleased = false;
        public bool NegativeEnergyCast = false;
    }

    #endregion

    #region Fields

    public float MinDamage, MaxDamage;
    public float StartupTime, ChargeTime, CooldownTime;
    public float minSize, maxSize;
    public float ActivationCost, FullChargeCost;
    public AnimationInfo AnimInfo =  new AnimationInfo();
    public Bullet Bullet;

    public List<SpellFX> OnActivateFX;
    private float coolDownStarted = 0;

    #endregion

    void Start()
    {
        //animation = GetComponent<GrowingOrbAnimation>();
    }

    private IEnumerator ChargeCoroutine(Transform SpellOrb, Transform Caster, Stats casterStats, Action action)
    {
        float startTime = Time.realtimeSinceStartup;
        AnimInfo.Activated = true;
        casterStats.EnergyRegenPause = true;

        SpellFX newFx = OnActivateFX[0].StartSpell(SpellOrb);
        GrowingOrbAnimation goa = newFx.GetComponent<GrowingOrbAnimation>();
        goa.GrowingBallParent = this;

        //foreach(SpellFX fx in OnActivateFX)
        //    fx.StartSpell(SpellOrb);

        StartCoroutine(ChargeAnimationInfoCoroutine(startTime));

        // If not enough energy for the full cast
        float negativeEnergy = casterStats.ConsumeEnergy(ActivationCost);
        if (negativeEnergy > 0)
            AnimInfo.NegativeEnergyCast = true;

        float dt = 0;
        float charge = 0;

        // Wait till button is released
        while (action.IsDown() || AnimInfo.NegativeEnergyCast)
        {
            dt = Time.realtimeSinceStartup - startTime;

            if (dt > StartupTime)
            {
                AnimInfo.State = AnimationInfo.AnimState.Overcharging;
                // reduce energy
            }

            yield return null;
        }

        AnimInfo.InputReleased = true;
        dt = Time.realtimeSinceStartup - startTime;

        // Wait atleast till startupTime
        if (dt < StartupTime)
            yield return new WaitForSeconds(StartupTime - dt);

        // Dt and charge calculation
        if (AnimInfo.State == AnimationInfo.AnimState.Overcharging)
            charge = AnimInfo.AnimStateProgress;

        // Gather Direction
        Vector3 dir = SpellOrb.position - Caster.position;
        dir.Normalize();

        // Consume energy and launch
        casterStats.ConsumeEnergy(FullChargeCost * charge);
        Bullet bullet = Bullet.Launch(SpellOrb.position, dir, Mathf.Lerp(MinDamage, MaxDamage, charge));

        float scale = minSize + (maxSize-minSize)*charge;
        bullet.transform.localScale = new Vector3(scale, scale, scale);

        AnimInfo.State = AnimationInfo.AnimState.Cooldown;
        coolDownStarted = Time.realtimeSinceStartup;

        casterStats.EnergyRegenPause = false;

        // Replace by Cooldown Animation Handler
        yield return new WaitForSeconds(CooldownTime);
        
        resetAnimationInfo();
    }

    private IEnumerator ChargeAnimationInfoCoroutine(float startTime)
    {
        float dt = 0;

        while (AnimInfo.Activated)
        {
            dt = Time.realtimeSinceStartup - startTime;

            switch (AnimInfo.State)
            {
                case AnimationInfo.AnimState.Starting:
                    AnimInfo.AnimStateProgress = Mathf.Max(0, Mathf.Min(1.0f, dt / StartupTime));
                    break;
                
                case AnimationInfo.AnimState.Cooldown:
                    AnimInfo.AnimStateProgress = Mathf.Max(0, Mathf.Min(1.0f, (Time.realtimeSinceStartup - coolDownStarted) / CooldownTime));
                    break;

                case AnimationInfo.AnimState.Overcharging:
                    AnimInfo.AnimStateProgress = Mathf.Max(0, Mathf.Min(1.0f, (dt - StartupTime) / ChargeTime));
                    break;
            }

            yield return null;
        }
    }

    private void resetAnimationInfo()
    {
        AnimInfo.State              = AnimationInfo.AnimState.Starting;
        AnimInfo.AnimStateProgress  = 0;

        AnimInfo.Activated          = false;
        AnimInfo.InputReleased      = false;
        AnimInfo.NegativeEnergyCast = false;
    }

    public override void Activate(Transform SpellOrb, Transform Caster, Stats casterStats, Action action)
    {
        if(!AnimInfo.Activated && casterStats.Energy > 0)
            StartCoroutine(ChargeCoroutine(SpellOrb, Caster, casterStats, action));
    }
}
