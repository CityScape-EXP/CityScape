using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawManager : MonoBehaviour
{
    public Sprite square;

    void Start()
    {
        DrawFloor();
        
    }

    private void Update()
    {
        // M -> 1층 Enemy 생성
        if(Input.GetKeyDown(KeyCode.M))
            DrawEnemy(0, 0);
        // K -> 2층 Enemy 생성
        if(Input.GetKeyDown(KeyCode.K)) 
            DrawEnemy(1, 0);
        // O -> 3층 Flying Enemy 생성
        if (Input.GetKeyDown(KeyCode.O))
            DrawEnemy(2, 1);
        // A -> 긴 플랫폼 생성
        if(Input.GetKeyDown(KeyCode.A))
            DrawPlatform(0, 0);
        // S -> 짧은 플랫폼 생성
        if(Input.GetKeyDown(KeyCode.S))
            DrawPlatform(0, 1);
    }

    // 바닥을 그려주는 함수. 설명 생략
    void DrawFloor()
    {
        Vector3 r_Start = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 r_End = Camera.main.ScreenToWorldPoint(new Vector3(2400, 180, 0));
        float width = Mathf.Abs(r_Start.x - r_End.x);
        float height = Mathf.Abs(r_Start.y - r_End.y);
        Vector2 centerPos = (r_Start + r_End) / 2f;
        GameObject floor = new GameObject();
        floor.name = "Floor";
        floor.AddComponent<SpriteRenderer>().sprite = square;
        floor.transform.position = centerPos;
        floor.transform.localScale = new Vector3(width, height, 1f);
    }

    // floor(층.. 아마 1층만 있음) , type를 받아 플랫폼 생성하고 반환하는 함수
    // type0:긴Platform  type1:짧은Platform
    void DrawPlatform(int floor, int type)
    {
        GameObject newObj = new GameObject() { name = "Platform" };
        if (type == 0)
        {
            InitObject(newObj, 2400, 70);
            newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2000 + 750, 445 + 300 * floor, 10));
        }
        else if (type == 1)
        {
            InitObject(newObj, 1500, 70);
            newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2000 + 1200, 445 + 300 * floor, 10));
        }
        newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;
        this.gameObject.GetComponent<PatternManager>().NewPattern(newObj, type, 0);
    }

    // floor(2는 공중), type를 받아 Enemy 생성하고 반환하는 함수
    // type0:Normal Enemy  type1:Flying Enemy
    void DrawEnemy(int floor, int type)
    {
        GameObject newObj = new GameObject() { name = "Enemy" };
        InitObject(newObj, 160, 210);
        newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.red;
        newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2000, 180 + 105 + 300 * floor, 10));
        this.gameObject.GetComponent<PatternManager>().NewPattern(newObj, type, floor);
    }

    // 게임 안에서 오브젝트가 생성되고 보여지기 위해 오브젝트에 초기화를 해주는 함수
    void InitObject(GameObject newObj, int width, int height)
    {
        Vector3 r_Start = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 r_End = Camera.main.ScreenToWorldPoint(new Vector3(width, height, 0));
        newObj.transform.localScale = new Vector3(r_End.x - r_Start.x, r_End.y - r_Start.y, 1f);
        newObj.AddComponent<SpriteRenderer>().sprite = square;
        newObj.AddComponent<Platform>().moveSpeed = 3f;
    }
}
