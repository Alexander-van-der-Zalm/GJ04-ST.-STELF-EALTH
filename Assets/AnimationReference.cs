using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AnimationReference : MonoBehaviour 
{
    //[System.Serializable]
    //public class AnimationRef
    //{
    //    public GameObject GameObject;
    //    public Vector3 OffsetPos;
    //    public float OrderingOffset;

    //    private Transform tr;
    //    private HandleSpriteOrdering ordering;

    //    public void Init()
    //    {
    //        tr = GameObject.transform;
    //        ordering = GameObject.GetComponent<HandleSpriteOrdering>();
    //        //if(order)
    //    }

    //    public void Update()
    //    {
    //        tr.localPosition = OffsetPos;
    //        ordering.YPosOffset = OffsetPos.y;
    //        ordering.Offset = (int)OrderingOffset;
    //    }
    //}

    public GameObject Head;
    public GameObject Shadow;
    public GameObject Item; 

    public Vector3 HeadPos;
    public Vector3 ShadowPos;
    public Vector3 ItemPos;

    public float ItemRotation;

    public float HeadOrderingOffset;
    public float ShadowOrderingOffset;
    public float ItemOrderingOffset;

    private Transform headTr;
    private Transform shadowTr;
    private Transform itemTr;

    private HandleSpriteOrdering headOrdering;
    private HandleSpriteOrdering shadowOrdering;
    private HandleSpriteOrdering itemOrdering;

    void Start()
    {
        headTr = Head.transform;
        shadowTr = Shadow.transform;
        itemTr = Item.transform;

        headOrdering = Head.GetComponent<HandleSpriteOrdering>();
        shadowOrdering = Shadow.GetComponent<HandleSpriteOrdering>();
        itemOrdering = Item.GetComponent<HandleSpriteOrdering>();
    }

    void Update()
    {
        headTr.localPosition = HeadPos;
        headOrdering.YPosOffset = HeadPos.y;
        headOrdering.Offset = (int)HeadOrderingOffset;

        shadowTr.localPosition = ShadowPos;
        shadowOrdering.YPosOffset = ShadowPos.y;
        shadowOrdering.Offset = (int)ShadowOrderingOffset;

        itemTr.localPosition = ItemPos;
        itemTr.localRotation = Quaternion.AngleAxis(ItemRotation,Vector3.forward);
        //ROt.SetAxisAngle()
        itemOrdering.YPosOffset = ItemPos.y;
        itemOrdering.Offset = (int)ItemOrderingOffset;

    }
}
