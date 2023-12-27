using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PrefebElement
{
    public string m_Name;
    public GameObject m_Prefeb;
}

public class MapBuilder : MonoBehaviour
{
    // ���� ������ (��, ��, �Ĺ� ������ �б���)
    public static MapBuilder instance = null;
    private int nowPhase;
    private int nowStage;
    private int nowPattern;
    private float patternStartTime;
    private float nowPatternTime;
    private float buildingTime; // 배경 건물 생성을 위한 Time

    [SerializeField]
    private GameObject building;
    
    public Sprite[] BackgroundBuildings;

    public void Init_var()
    {
        nowPhase = 0;
        patternStartTime = 0f;
        nowPatternTime = 0f;
        buildingTime = 0f;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Init_var();
    }

    private void Update()
    {
        // GameManager.instance.stageTime -> 스테이지가 시작된 후 흐른 시간
        float ingameTime = GameManager.instance.stageTime;

        // 현재 Scene의 이름이 TestScene(인게임) 일 경우에만 실행한다
        if (!(SceneManager.GetActiveScene().name == "TestScene"))
            return;

        // 할당된 패턴 시간이 지났을 경우 (패턴 사이 시간 간격은 3초이다)
        if(Time.time - patternStartTime > nowPatternTime + 2)
        {
            // 초반 5패턴 & 중반 8패턴 & 후반 6패턴
            if (ingameTime < 30) nowPhase = 0;
            else if (ingameTime < 85) nowPhase = 1;
            else if (ingameTime < 120) nowPhase = 2;
            else Debug.Log("게임 클리어");

            // 가장 어려운 패턴 번호 : 3 으로 두고 연속 출연 안되게 수정
            if (nowPattern == 3)
                nowPattern = Random.Range(0, 3);
            else
                nowPattern = Random.Range(0, 4);
            Debug.Log($"페이즈 : {nowPhase} // 패턴 : {nowPattern}");
            PatternData patterndata = GameManager.instance.dm.GetPatternData(0, nowPhase, nowPattern);
            DrawPattern(patterndata);

            nowPatternTime = patterndata.patternTime;
            patternStartTime = Time.time;
        }
        // 배경 뒤의 빌딩 생성 코드
        if(Time.time - buildingTime > 5)
        {
            int buildingNum = Random.Range(0, 3);
            GameObject newBuilding = Instantiate(building, new Vector3(30, 4.4f, 0), Quaternion.identity);
            newBuilding.GetComponent<SpriteRenderer>().sprite = BackgroundBuildings[buildingNum];
            buildingTime = Time.time;
        }
    }

    private void DrawPattern(PatternData pd)
    {
        // Platform 생성
        foreach (var platform in pd.p_Data)
        {
            GameObject platformObject = PoolManager.GetObject(5 + platform.type);
            Vector3 platformPos;
            platformObject.SetActive(true);
            if(platform.type == 0)
            {
                platformPos = Camera.main.ScreenToWorldPoint(new Vector3(2000, 445, 10));
            }
            else
            {
                platformPos = Camera.main.ScreenToWorldPoint(new Vector3(1600, 445, 10));
            }
            platformObject.transform.position = new Vector3(platform.pos + platformPos.x + 20, platformPos.y, 0);
        }
        // Enemy 생성
        foreach (var enemy in pd.e_Data)
        {
            GameObject enemyObject = PoolManager.GetObject(2 + enemy.type);
            Vector3 enemPos = Camera.main.ScreenToWorldPoint(new Vector3(2000, 285 + 300 * enemy.floor, 0));
            enemyObject.transform.position = new Vector3(enemPos.x + enemy.x_pos + 20, enemPos.y, 0);
        }
    }

    
}
