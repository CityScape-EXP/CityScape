using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;

public class UI_Base : MonoBehaviour
{
    /// <summary>
    /// 타겟 기종 (S23 Ultra) 기준 해상도
    /// </summary>
    const float StandardWidth = 3088f;
    /// <summary>
    /// TMP_Text의 폰트 사이즈 조정용 비율
    /// </summary>
    static float ratio = 1f;

    /// <summary>
    /// UI 오브젝트를 캐싱
    /// </summary>
    protected Dictionary<Type, UnityEngine.Object[]> typecache = new Dictionary<Type, UnityEngine.Object[]>();

    /// <summary>
    /// 오브젝트 바인딩 (enum 기반)
    /// </summary>
    /// <typeparam name="T"> 컴포넌트 타입 </typeparam>
    /// <param name="type"> enum (names) </param>
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] typenames = Enum.GetNames(type);
        UnityEngine.Object[] ui_objects =  new UnityEngine.Object[typenames.Length];
        typecache.Add(typeof(T), ui_objects);

        for(int i = 0;i < typenames.Length; i++)
        {
            ui_objects[i] = Utils.FindChild<T>(gameObject, typenames[i], true);


            if (ui_objects[i] == null)
            {
                Debug.LogError($"{typeof(T).Name} 타입의 {typenames[i]} 을 찾을 수 없음");
            }
            else if(typeof(T) == typeof(TMP_Text))
            {
                ratio = Screen.width / StandardWidth;
                TMP_Text text = ui_objects[i] as TMP_Text;
                text.fontSize = text.fontSize * ratio;
            }
        }
    }

    /// <summary>
    /// UI 이벤트 바인딩
    /// </summary>
    /// <param name="gameObject"> 타겟 오브젝트 </param>
    /// <param name="evtAction"> 이벤트 </param>
    /// <param name="evtType"> 이벤트 타입 Default = Click</param>
    protected void BindEvent(GameObject gameObject, Action<PointerEventData> evtAction, Define.UIEvent evtType = Define.UIEvent.Click)
    {
        UI_EventHandler evt = gameObject.AddComponent<UI_EventHandler>();

        switch(evtType) 
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= evtAction; //혹시? 이미 추가되어있어서 중복으로 나올까봐
                evt.OnClickHandler += evtAction;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= evtAction;
                evt.OnDragHandler += evtAction;
                break;
            case Define.UIEvent.DragEnd:
                evt.OnDragEndHandler -= evtAction;
                evt.OnDragEndHandler += evtAction;
                break;
        }

    }
    /// <summary>
    /// cache 내 컴포넌트 Get
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected T Get<T> (int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (typecache.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }


}
