using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler

{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnDragEndHandler = null;
    PointerEventData _eventData = null;
    bool isDargging = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke(eventData);  //  ?연산자는 null이 아닐경우에만 실행
    }
    public void OnDrag(PointerEventData eventData)
    {
        //OnDragHandler?.Invoke(eventData);
        _eventData = eventData;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEndHandler?.Invoke(eventData);

        isDargging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDargging = true;
        _eventData = eventData;
    }

    private void Update()
    {
        if (isDargging) 
        {
            OnDragHandler?.Invoke(_eventData);
        }
    }
}
