using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratio : MonoBehaviour  // 인게임에서 화면 비율에 따라 카메라의 위치를 바꿔서 해상도에 대응하는 스크립트
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)2400 / 1080); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }
    void OnPreCull() => GL.Clear(true, true, Color.black); // 카메라가 비추는 이외의 공간을 검은색으로 칠함
}
