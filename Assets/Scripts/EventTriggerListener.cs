using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler,IPointerDownHandler
{
    public UnityAction onClick;
    public UnityAction onPressDown;
    public UnityAction<PointerEventData> onDrag;
    public UnityAction<PointerEventData> onBeginDrag;
    public UnityAction<PointerEventData> onPointerUp;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(eventData);
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUp != null)
        {
            onPointerUp(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPressDown != null)
        {
            onPressDown( );
        }
    }
}