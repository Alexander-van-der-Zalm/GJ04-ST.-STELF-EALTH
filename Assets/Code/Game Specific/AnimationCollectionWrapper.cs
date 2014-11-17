using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
//public struct AnimatorParameter
//{
//    public enum APType
//    {
//        Bool,
//        Trigger,
//        Int,
//        Float
//    }

//    public APType Type;
//    public string ParameterName;
//}

[System.Serializable]
public class AnimatorCollectionWrapper 
{
    [SerializeField]
    protected List<Animator> animators;

    public AnimatorCollectionWrapper(GameObject rootGO)
    {
        animators = rootGO.GetComponentsInChildren<Animator>().ToList();
    }

    #region Get Set

    #region Float

    public float GetFloat(string param)
    {
        return animators.First().GetFloat(param);
    }

    public void SetFloat(string param, float value)
    {
        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].SetFloat(param, value);
        }
            
    }

    #endregion

    #region Bool

    public bool GetBool(string param)
    {
        return animators.First().GetBool(param);
    }

    public void SetBool(string param, bool value)
    {
        for (int i = 0; i < animators.Count; i++)
            animators[i].SetBool(param, value);
    }

    #endregion

    #region Trigger

    public void SetTrigger(string param)
    {
        for (int i = 0; i < animators.Count; i++)
            animators[i].SetTrigger(param);
    }

    #endregion

    #region Int

    public int GetInt(string param)
    {
        return animators.First().GetInteger(param);
    }

    public void SetInt(string param, int value)
    {
        for (int i = 0; i < animators.Count; i++)
            animators[i].SetInteger(param, value);
    }

    #endregion

    #endregion
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
        get { return GetFloat(velocityX); }
        set { SetFloat(velocityX, value); }
    }

    public float VelocityY
    {
        get { return GetFloat(velocityY); }
        set { SetFloat(velocityY, value); }
    }

    public float VelocityXAbs
    {
        get { return GetFloat(velocityXAbs); }
        set { SetFloat(velocityXAbs, value); }
    }

    public float InputX
    {
        get { return GetFloat(inputX); }
        set { SetFloat(inputX, value); }
    }

    public float InputY
    {
        get { return GetFloat(inputY); }
        set { SetFloat(inputY, value); }
    }

    public float LookY
    {
        get { return GetFloat(lookY); }
        set { SetFloat(lookY, value); }
    }
    public float LookXAbs
    {
        get { return GetFloat(lookXAbs); }
        set { SetFloat(lookXAbs, value); }
    }

    public bool Drink
    {
        get { return GetBool(drink); }
        set { SetBool(drink,value); }
    }
    public bool Dance
    {
        get { return GetBool(dance); }
        set { SetBool(dance, value); }
    }

    public bool Talk
    {
        get { return GetBool(talk); }
        set { SetBool(talk, value); }
    }

    public bool Bump
    {
        get { return GetBool(bump); }
        set { SetBool(bump,value); }
    }

    public bool StealTrigger
    {
        //get { return animators.First().Get(stealTrigger); }
        set { SetTrigger(stealTrigger); }
    }

    public bool Give
    {
        get { return GetBool(give); }
        set { SetBool(give, value); }
    }
    public bool Recieve
    {
        get { return GetBool(recieve); }
        set { SetBool(recieve, value); }
    }
    public bool Moving
    {
        get { return GetBool(moving); }
        set { SetBool(moving, value); }
    }
    public bool Angry
    {
        get { return GetBool(angry); }
        set { SetBool(angry, value); }
    }
}
