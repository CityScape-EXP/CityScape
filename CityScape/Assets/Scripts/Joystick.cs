using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    private RectTransform joystickTransform;

    [SerializeField]
    private float dragThreshold = 0.6f;
    [SerializeField]
    private int dragMovementDistance = 30;
    [SerializeField]
    private int dragOffsetDistance = 100; //조이스틱의 이동반경을 결정

    public event Action<Vector2> OnMove;

    //싱글톤 적용
    public static Joystick instance;
    
    private void Awake(){
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
        }
        joystickTransform = (RectTransform)transform;
    }

    public void OnDrag(PointerEventData eventData){
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickTransform,
            eventData.position,
            null,
            out offset);
        //이동반경 제한. Panel 안에서만 활동 가능
        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
        Debug.Log(offset);
        joystickTransform.anchoredPosition = offset * dragMovementDistance; //조이스틱 움직임

        Vector2 inputVector = CalculateMovementInput(offset);
        OnMove?.Invoke(inputVector);
    }

    private Vector2 CalculateMovementInput(Vector2 offset){
        float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;
        return new Vector2(x,y);
    }

    public void OnPointerDown(PointerEventData eventData){ //조이스틱 원래 위치로

    }

    public void OnPointerUp(PointerEventData eventData){
        joystickTransform.anchoredPosition = Vector2.zero; //초기위치 (0,0)
        OnMove?.Invoke(Vector2.zero);
    }
}