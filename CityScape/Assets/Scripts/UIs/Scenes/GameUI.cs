using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameUI : UI_Base
{

    enum GameObjects 
    {
        joystickBG,
        joystickHandle,
    }

    enum Sliders 
    { 
        TimeSlider 
    }


    enum Texts
    {
    }
    enum Buttons
    {
        Jump,
        PauseButton,
        Roll,
    }



    // Start is called before the first frame update
    void Start()
    {
        //Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Slider>(typeof(Sliders));

        BindEvent(Get<Button>((int)Buttons.Jump).gameObject, Jump);
        BindEvent(Get<Button>((int)Buttons.PauseButton).gameObject, PauseButton);
        BindEvent(Get<Button>((int)Buttons.Roll).gameObject, Roll);
        JoystickBind();

    }

    void Update()
    {
        Get<Slider>((int)Sliders.TimeSlider).value = GameManager.instance.stageTime / GRManager.instance.gameTime;
    }

    #region Joystick
    /// <summary>
    /// joystick handle 기본 위치
    /// </summary>
    Vector2 _joystickPivotPos;

    /// <summary>
    /// joystick 최대 이동 거리
    /// </summary>
    float _joystickLimit;
    /// <summary>
    /// joystick handle
    /// </summary>
    GameObject _joystickHandle;

    /// <summary>
    /// joystick 방향 벡터
    /// </summary>
    Vector2 _directionVector = Vector2.zero;

    void JoystickBind()
    {
        GameObject joystickBG = Get<GameObject>((int)GameObjects.joystickBG);
        _joystickHandle = Get<GameObject>((int)GameObjects.joystickHandle);

        //기본 위치와 최대 이동 거리 계산
        _joystickPivotPos = _joystickHandle.transform.position;
        _joystickLimit = ((joystickBG.transform as RectTransform).rect.width - (_joystickHandle.transform as RectTransform).rect.width) / 2f;

        //이벤트 bind
        BindEvent(_joystickHandle, JoystickDrag, Define.UIEvent.Drag);
        BindEvent(_joystickHandle, JoystickDragEnd, Define.UIEvent.DragEnd);
    }

    /// <summary>
    /// 조이스틱 드래그
    /// </summary>
    /// <param name="evt"></param>
    void JoystickDrag(PointerEventData evt)
    {
        _directionVector = (evt.position - _joystickPivotPos).normalized;
        _joystickHandle.transform.position = _joystickPivotPos + _directionVector * Mathf.Min((evt.position - _joystickPivotPos).magnitude, 50);

        Player.instance.SetDirectionMove(_directionVector);

    }

    /// <summary>
    /// 조이스틱 드래그 종료
    /// </summary>
    /// <param name="evt"></param>
    void JoystickDragEnd(PointerEventData evt)
    {
        _directionVector = Vector2.zero;
        _joystickHandle.transform.position = _joystickPivotPos;
        //GameManager.Sound.StopWalkPlay();
        //_player?.StopMove();
    }

    #endregion Joystick
    void Jump(PointerEventData evt)
    {
        Player.instance.JumpUp();
    }
    void PauseButton(PointerEventData evt)
    {
        if(GameManager.instance.stageNum==1)
            UIManager.LoadUI(Define.UI_Type.PauseUI1);
        else if (GameManager.instance.stageNum == 2)
            UIManager.LoadUI(Define.UI_Type.PauseUI2);
        else
            UIManager.LoadUI(Define.UI_Type.PauseUI3);
        Time.timeScale = 0f;
        UIManager.pauseOnclicked = true;
        GameManager.Sound.Play(Define.SFX.UI_select_1);
    }
    void Roll(PointerEventData evt)
    {
        Player.instance.StartRolling();
    }
}
