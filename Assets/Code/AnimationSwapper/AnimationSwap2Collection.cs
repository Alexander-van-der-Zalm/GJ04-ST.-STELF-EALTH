using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class AnimationSwap2Collection : EasyScriptableObject<AnimationSwap2Collection> 
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

    //TODO
    //[SerializeField,HideInInspector]
    //m_overrideController =

    [MenuItem("CustomTools/Create new AnimationSwapCollection")]
    public static void CreateAnimationSwapCollection()
    {
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/AnimationSwapCollection.asset");
        AnimationSwap2Collection.CreateObjAndAsset(path);
    }

    public override void Init(HideFlags newHideFlag = HideFlags.None)
    {
        base.Init(newHideFlag);
        BodyVarieties = new List<AS_AnimIndexCollection>();
        HeadVarieties = new List<AS_AnimListCollection>();
    }

    public AnimatorOverrideController GetNewOverrideController()
    {
        AnimatorOverrideController controller = new AnimatorOverrideController();
        controller.runtimeAnimatorController = Controller;
        return controller;
    }

    public void ApplyControllerChange()
    {

    }

    public void SetAnimator(Animator head, Animator body,  int headIndex,int bodyIndex)
    {
        AnimatorOverrideController controller = GetNewOverrideController();

        //Loop to set all the animations
        for (int i = 0; i < controller.clips.Length; i++)
        {
            Debug.Log(controller.clips[i].originalClip.name);

            //overide.clips[i].overrideClip = 
        }
            

        // Set all the overrided animations to the animator
        //head.runtimeAnimatorController = overideHead;
    }

    public void AddNewBodyVariety()
    {
        AnimatorOverrideController controller = GetNewOverrideController();
        
        AS_AnimIndexCollection newBody = new AS_AnimIndexCollection();

        for (int i = 0; i < controller.clips.Length; i++)
        {
            newBody.Animations.Add(new AS_AnimIndex());
        }

        BodyVarieties.Add(newBody);
    }

    public void AddNewHeadVariety()
    {
        AnimatorOverrideController controller = GetNewOverrideController();
        
        AS_AnimListCollection newHead = new AS_AnimListCollection();

        for (int i = 0; i < controller.clips.Length; i++)
        {
            newHead.Animations.Add(new AS_AnimList());
        }

        HeadVarieties.Add(newHead);
    }

    //public void 
}
