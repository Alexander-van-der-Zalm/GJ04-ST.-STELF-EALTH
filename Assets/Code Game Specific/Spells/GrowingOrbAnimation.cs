using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpellFX))]
public class GrowingOrbAnimation : MonoBehaviour 
{
    public float StartScale, MinScale, MaxScale;

    public float NormalRotationPSec, OverchargeRotationPsec, CoolDownRotationPSec;

    public Color castColor, cdColor;

    public GrowingBall GrowingBallParent { set { gb = value; } }
    private GrowingBall gb;
    private SpellFX fx;
    private Material mat;

	// Use this for initialization
	void Start () 
    {
        //gb = gameObject.GetComponent<GrowingBall>();
        fx = gameObject.GetComponent<SpellFX>();
        mat = renderer.material;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (gb == null)
            return;
        
        if (gb.AnimInfo.AnimStateProgress == 0 && gb.AnimInfo.State == GrowingBall.AnimationInfo.AnimState.Starting && fx.SpellStart)
        {
            fx.SpellStart = false;
            Debug.Log("DEACTIVATE");
            gameObject.SetActive(false);
        }
        else if(fx.SpellStart)
            gameObject.SetActive(true);

        float rotation = 0;
        mat.color = castColor;

        switch (gb.AnimInfo.State)
        {
            case GrowingBall.AnimationInfo.AnimState.Starting:
                setSize(StartScale + gb.AnimInfo.AnimStateProgress * (MinScale - StartScale));
                rotation = NormalRotationPSec * Time.deltaTime;
                break;
            case GrowingBall.AnimationInfo.AnimState.Overcharging:
                setSize(MinScale + gb.AnimInfo.AnimStateProgress * (MaxScale - MinScale));
                rotation = OverchargeRotationPsec * Time.deltaTime;
                break;
            case GrowingBall.AnimationInfo.AnimState.Cooldown:
                setSize(transform.localScale.z * 1.1f);

                Color color = cdColor;
                color.a = 1 - gb.AnimInfo.AnimStateProgress;
                mat.color = color;

                rotation = CoolDownRotationPSec * Time.deltaTime;
                break;
        }

        transform.Rotate(0, rotation, 0);
	}

    private void setSize(float p)
    {
        transform.localScale = new Vector3(p,p,p);
    }
}
