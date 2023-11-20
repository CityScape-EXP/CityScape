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
        if(Input.GetKeyDown(KeyCode.A))
            DrawEnemy(0);
        if(Input.GetKeyDown(KeyCode.Q)) 
            DrawEnemy(1);
        if(Input.GetKeyDown(KeyCode.L))
            DrawPlatform(0);
        if(Input.GetKeyDown(KeyCode.P))
            DrawPlatform(1);
    }

    void DrawFloor()
    {
        Vector3 r_Start = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 r_End = Camera.main.ScreenToWorldPoint(new Vector3(2400, 180, 0));
        float width = Mathf.Abs(r_Start.x - r_End.x);
        float height = Mathf.Abs(r_Start.y - r_End.y);
        Vector2 centerPos = (r_Start + r_End) / 2f;
        GameObject floor = new GameObject();
        floor.name = "Floor";
        InitObject(floor);
        floor.transform.position = centerPos;
        floor.transform.localScale = new Vector3(width, height, 1f);
    }

    GameObject DrawPlatform(int floor)
    {
        Vector3 r_Start = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 r_End = Camera.main.ScreenToWorldPoint(new Vector3(2400, 70, 0));
        float width = Mathf.Abs(r_Start.x - r_End.x);
        float height = Mathf.Abs(r_Start.y - r_End.y);
        GameObject newObj = new GameObject();
        newObj.name = "Platform";
        InitObject(newObj);
        newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;
        newObj.AddComponent<Platform>().moveSpeed = 5f;
        newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2000 + 1200, 445 + 300 * floor, 10));
        newObj.transform.localScale = new Vector3(width, height, 1f);
        return newObj;
    }

    GameObject DrawEnemy(int floor)
    {
        Vector3 r_Start = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 r_End = Camera.main.ScreenToWorldPoint(new Vector3(160, 210, 0));
        float width = Mathf.Abs(r_Start.x - r_End.x);
        float height = Mathf.Abs(r_Start.y - r_End.y);
        GameObject newObj = new GameObject();
        newObj.name = "Enemy";
        InitObject(newObj);
        newObj.GetComponent<SpriteRenderer>().color = UnityEngine.Color.red;
        newObj.AddComponent<Platform>().moveSpeed = 5f;
        newObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(2000, 180 + 105 + 300 * floor, 10));
        newObj.transform.localScale = new Vector3(width, height, 1f);
        return newObj;
    }

    void InitObject(GameObject newObj)
    {
        newObj.AddComponent<SpriteRenderer>();
        newObj.GetComponent<SpriteRenderer>().sprite = square;
    }
}
