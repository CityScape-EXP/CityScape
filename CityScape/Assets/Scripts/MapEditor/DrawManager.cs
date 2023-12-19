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
        DrawLine();        
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
        // N -> 1층 강화 Enemy 생성
        if (Input.GetKeyDown(KeyCode.N))
            DrawEnemy(0, 2);
        // J -> 2층 강화 Enemey 생성
        if (Input.GetKeyDown(KeyCode.J))
            DrawEnemy(1, 2);
        // A -> 긴 플랫폼 생성
        if (Input.GetKeyDown(KeyCode.A))
            DrawPlatform(0, 0);
        // S -> 짧은 플랫폼 생성
        if(Input.GetKeyDown(KeyCode.S))
            DrawPlatform(0, 1);
        
    }

    // 생성 위치 표시하는 라인 그려주는 함수. 설명 생략
    void DrawLine()
    {
        Vector3 l_Start = Camera.main.ScreenToWorldPoint(new Vector3(1995, 0, 0));
        Vector3 l_End = Camera.main.ScreenToWorldPoint(new Vector3(2005, 1080, 0));
        Vector2 centerPos = (l_Start + l_End) / 2f;
        GameObject line = new GameObject();
        line.name = "Line";
        line.AddComponent<SpriteRenderer>().sprite = square;
        line.GetComponent<SpriteRenderer>().color = UnityEngine.Color.red;
        line.GetComponent<SpriteRenderer>().sortingOrder = 3;
        line.transform.position = centerPos;
        line.transform.localScale = new Vector3(Mathf.Abs(l_Start.x - l_End.x), Mathf.Abs(l_Start.y - l_End.y), 1f);
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
        floor.GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;
    }

    // floor(층.. 아마 1층만 있음) , type를 받아 플랫폼 생성하고 반환하는 함수
    // type0:긴Platform  type1:짧은Platform
    void DrawPlatform(int floor, int type)
    {
        GameObject newObj = new GameObject() { name = "Platform" };
        if (type == 0)
        {
            InitObject(newObj, 2400, 70);
            newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2000 + 1200, 445 + 300 * floor, 10));
        }
        else if (type == 1)
        {
            InitObject(newObj, 1500, 70);
            newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2000 + 750, 445 + 300 * floor, 10));
        }
        newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;
        this.gameObject.GetComponent<PatternManager>().NewPattern(newObj, type, 0);
    }

    // floor(2는 공중), type를 받아 Enemy 생성하고 반환하는 함수
    // type0:Normal Enemy  type1:Flying Enemy type2:Enhanced Enemy
    void DrawEnemy(int floor, int type)
    {
        GameObject newObj = new GameObject() { name = "Enemy" };
        InitObject(newObj, 160, 210);
        switch(type)
        {
            case 0:
                newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
                break;
            case 1:
                newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.blue;
                break;
            case 2:
                newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                break;
        }
        newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2080, 180 + 105 + 300 * floor, 10));
        this.gameObject.GetComponent<PatternManager>().NewPattern(newObj, type, floor);
    }

    // 게임 안에서 오브젝트가 생성되고 보여지기 위해 오브젝트에 초기화를 해주는 함수
    void InitObject(GameObject newObj, int width, int height)
    {
        Vector3 r_Start = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 r_End = Camera.main.ScreenToWorldPoint(new Vector3(width, height, 0));
        newObj.transform.localScale = new Vector3(r_End.x - r_Start.x, r_End.y - r_Start.y, 1f);
        //Debug.Log($"{r_End.x - r_Start.x},{r_End.y - r_Start.y}");
        newObj.AddComponent<SpriteRenderer>().sprite = square;
        newObj.AddComponent<Platform>().moveSpeed = 3f;
    }
}
