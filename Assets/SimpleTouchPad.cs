using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 origin;
    private Vector3 direction;

    public float smoothing;
    public Vector3 smothDirection;

    private void Awake()
    {
       direction = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        origin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPosition = eventData.position;
        Vector3 directionRaw = currentPosition - origin;
        direction = directionRaw.normalized;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        direction = Vector3.zero;
    }

    public Vector3 GetDirection()
    {
        smothDirection = Vector3.MoveTowards(smothDirection, direction, smoothing);
        return smothDirection;
    }
}
