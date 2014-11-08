using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AnimatorCollectionWrapper 
{
    [SerializeField]
    protected List<Animator> animators;

    public AnimatorCollectionWrapper(GameObject rootGO)
    {
        animators = rootGO.GetComponentsInChildren<Animator>().ToList();
    }
}

[System.Serializable]
public class AnimationSwapAnimatorWrapper:AnimatorCollectionWrapper
{
    // Parameters
    private string velocityX = "VelocityX";
    private string velocityY = "VelocityY";
    private string inputX = "InputX";
    private string inputY = "InputY";
    private string drink = "Drink";
    private string dance = "Dance";
    private string talk = "Talk";
    private string bump = "Bump";
    private string stealTrigger = "StealTrigger";
    private string give = "Give";
    private string recieve = "Recieve";
    private string velocityXAbs = "VelocityXAbs";
    private string lookXAbs = "LookXAbs";
    private string lookY = "LookY";
    private string moving = "Moving";
    private string angry = "Angry";

    public AnimationSwapAnimatorWrapper(GameObject rootGO) : base(rootGO) { }

    public float VelocityX
    {
        get { return animators.First().GetFloat(velocityX); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetFloat(velocityX, value); }
    }

    public float VelocityY
    {
        get { return animators.First().GetFloat(velocityY); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetFloat(velocityY, value); }
    }

    public float VelocityXAbs
    {
        get { return animators.First().GetFloat(velocityXAbs); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetFloat(velocityXAbs, value); }
    }

    public float InputX
    {
        get { return animators.First().GetFloat(inputX); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetFloat(inputX, value); }
    }

    public float InputY
    {
        get { return animators.First().GetFloat(inputY); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetFloat(inputY, value); }
    }

    public float LookY
    {
        get { return animators.First().GetFloat(lookY); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetFloat(lookY, value); }
    }
    public float LookXAbs
    {
        get { return animators.First().GetFloat(lookXAbs); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetFloat(lookXAbs, value); }
    }

    public bool Drink
    {
        get { return animators.First().GetBool(drink); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetBool(drink,value); }
    }
    public bool Dance
    {
        get { return animators.First().GetBool(dance); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetBool(dance, value); }
    }

    public bool Talk
    {
        get { return animators.First().GetBool(talk); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetBool(talk, value); }
    }

    public bool Bump
    {
        get { return animators.First().GetBool(bump); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetTrigger(bump); }
    }

    public bool StealTrigger
    {
        //get { return animators.First().Get(stealTrigger); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetTrigger(stealTrigger); }
    }

    public bool Give
    {
        get { return animators.First().GetBool(give); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetBool(give, value); }
    }
    public bool Recieve
    {
        get { return animators.First().GetBool(recieve); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetBool(recieve, value); }
    }
    public bool Moving
    {
        get { return animators.First().GetBool(moving); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetBool(moving, value); }
    }
    public bool Angry
    {
        get { return animators.First().GetBool(angry); }
        set { for (int i = 0; i < animators.Count; i++)animators[i].SetBool(angry, value); }
    }
}
