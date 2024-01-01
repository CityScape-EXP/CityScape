using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField] int backgroundDepth;
    [SerializeField] bool isChildren;
    float speed; // 배경 이동 속도
    float posValue = 80f; // 배경 무한반복을 위한 위치 저장 변수

    Sprite[] backgroundStageImage = new Sprite[3];
    SpriteRenderer spriteRenderer;

    Vector2 startPos;
    float newPos;

    void Awake()
    {
        InitImage();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = Mathf.Min(7 - 2 * backgroundDepth, 5);
        startPos = transform.position;
    }

    private void OnEnable()
    {
        if (backgroundDepth <= 0)
            return;
        Debug.Log("이미지 변경");
        spriteRenderer.sprite = backgroundStageImage[GameManager.instance.stageNum - 1];
    }

    private void Update()
    {
        if (isChildren) return;
        newPos = Mathf.Repeat(Time.time * speed, posValue);
        transform.position = startPos + Vector2.left * newPos;
    }

    void InitImage()
    {
        if (backgroundDepth <= 0)
            return;

        for(int i = 0; i< 3; i++)
        {
            backgroundStageImage[i] = Resources.Load<Sprite>($"Sprites/Stage0{i+1}/background{backgroundDepth}");
            if(backgroundStageImage[i] == null)
            {
                Debug.Log($"Stage{i + 1}, Depth{backgroundDepth}의 이미지에서 Null 발생");
            }
        }
    }
}
