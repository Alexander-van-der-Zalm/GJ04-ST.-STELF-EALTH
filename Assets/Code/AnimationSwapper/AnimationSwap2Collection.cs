using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AnimationSwap2Collection : MonoBehaviour 
{
    //[SerializeField]
    //public List<string> AnimationNames;
    //[SerializeField]
    //public RuntimeAnimationController Controller; 

    [SerializeField]
    public RuntimeAnimatorController Controller;

    [SerializeField]
    public List<AS_AnimIndexCollection> BodyVarieties;

    [SerializeField]
    public List<AS_AnimListCollection> HeadVarieties;

    public void ApplyControllerChange()
    {

    }

    public void SetAnimator(Animator head, Animator body,  int headIndex,int bodyIndex)
    {
        AnimatorOverrideController overideHead = new AnimatorOverrideController();
        overideHead.runtimeAnimatorController = Controller;

        //Loop to set all the animations
        for (int i = 0; i < overideHead.clips.Length; i++)
        {
            Debug.Log(overideHead.clips[i].originalClip.name);
            //overide.clips[i].overrideClip = 
        }
            

        // Set all the overrided animations to the animator
        //head.runtimeAnimatorController = overideHead;
    }

    public void AddNewBodyVariety()
    {
        AS_AnimIndexCollection newBody = new AS_AnimIndexCollection();
        //for(int i = 0; i < AnimationNames.Count; i++)
        //{
        //    newBody.Animations.Add(new AS_AnimIndex());
        //}
        BodyVarieties.Add(newBody);
    }

    public void AddNewHeadVariety()
    {
        AS_AnimListCollection newHead = new AS_AnimListCollection();
        //for(int i = 0; i < AnimationNames.Count; i++)
        //{
        //    newHead.Animations.Add(new AS_AnimList());
        //}
        HeadVarieties.Add(newHead);
    }

    public void UpdateAllVarietyNames()
    {

    }

    //public void 
}
