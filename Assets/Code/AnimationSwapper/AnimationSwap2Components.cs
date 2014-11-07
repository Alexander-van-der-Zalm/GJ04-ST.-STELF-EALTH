using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AS_AnimList
{
    [SerializeField]
    List<AnimationClip> Animations;
}

[System.Serializable]
public class AS_AnimListCollection
{
    [SerializeField]
    public List<AS_AnimList> Animations;
}

[System.Serializable]
public class AS_AnimIndex
{
    [SerializeField]
    public AnimationClip Animation;

    [SerializeField]
    public int Index;

    public void OnGui()
    {

    }
}

[System.Serializable]
public class AS_AnimIndexCollection
{
    [SerializeField]
    public List<AS_AnimIndex> Animations;
}
