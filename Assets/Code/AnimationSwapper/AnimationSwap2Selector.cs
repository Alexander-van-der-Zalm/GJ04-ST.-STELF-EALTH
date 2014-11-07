using UnityEngine;
using System.Collections;

[System.Serializable,ExecuteInEditMode]
public class AnimationSwap2Selector : MonoBehaviour 
{
    [SerializeField]
    public AnimationSwap2Collection CollectionReference;

    [SerializeField]
    private AnimatorOverrideController overrideController;// = new AnimatorOverrideController();

    [SerializeField]
    public int HeadVariety;

    [SerializeField]
    public int BodyVariety;


	// Use this for initialization
	void Start () 
    {
        Animator anim = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;

        //overrideController.runtimeAnimatorController = anim;
        //anim.runtimeAnimatorController = overrideController;

        //for (int i = 0; i < overrideController.clips.Length; i++)
        //{
        //    Debug.Log(overrideController.clips[i].originalClip.name);
        //}

        Debug.Log("  aa" + CollectionReference.BodyVarieties[0].Animations[0].Animation.name);

        overrideController.clips[0].overrideClip = CollectionReference.BodyVarieties[0].Animations[0].Animation;

        overrideController["NPC01_A_Walk_Side"] = CollectionReference.BodyVarieties[0].Animations[0].Animation;

        anim.runtimeAnimatorController = overrideController;
	}
	
}
