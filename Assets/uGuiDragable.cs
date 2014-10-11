using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Drag UI entities around
/// </summary>
[ExecuteInEditMode]
public class uGuiDragable : MonoBehaviour,IBeginDragHandler, IDragHandler
{
    private RectTransform rtr = null;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        // get a reference to the RectTransform
        rtr = GetComponent<RectTransform>();
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Set offset for drag
        offset = rtr.position - Camera.main.ScreenToWorldPoint(eventData.pressPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the dragable entity
        rtr.position = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
    }
}
