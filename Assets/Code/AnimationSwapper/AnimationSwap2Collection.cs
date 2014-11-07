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

    public RuntimeAnimatorController Controller;

    [SerializeField]
    public List<AS_AnimIndexCollection> BodyVarieties;

    [SerializeField]
    public List<AS_AnimListCollection> HeadVarieties;

    // Property get private set

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
