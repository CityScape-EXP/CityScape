using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatternManager : MonoBehaviour
{
    // 편하게 사용할 수 있게 자체참조
    public static PatternManager instance;

    // 패턴리스트 -> Draw된 패턴들의 GameObject 정보를 저장한다
    private List<GameObject> patternList = new List<GameObject>();
    // 타입리스트 -> 패턴들이 Draw된 타입을 저장한다.
    // 플랫폼 - 0:긴플랫폼 1:짧은플랫폼
    private List<int> typeList = new List<int>();
    private List<int> floorList = new List<int>();
    float startPos_x;

    // 코드가 짧아서 UI도 같이 넣어버림
    public GameObject stageTextObj, phaseTextObj, patternTextObj;
    TMP_Text stageText, phaseText, patternText;
    int nowStage = 0; int nowPhase = 0; int nowPattern = 0;

    private void Update()
    {
        // 다른 플랫폼과의 위치 차이를 파악하기 위해 첫번째 오브젝트의 x좌표를 계속 갱신한다
        if (patternList.Count != 0)
            startPos_x = patternList[0].transform.position.x;
    }

    private void Start()
    {
        instance = this;
        stageText = stageTextObj.GetComponent<TMP_Text>();
        phaseText = phaseTextObj.GetComponent<TMP_Text>();
        patternText = patternTextObj.GetComponent<TMP_Text>();
        patternList = new List<GameObject>();
    }

    // DrawManager에서 PatternManager로 Object 정보를 위임한다
    public void NewPattern(GameObject obj, int type, int floor) 
    {
        patternList.Add(obj); 
        typeList.Add(type);
        floorList.Add(floor);
    }

    // Reset 버튼 눌렸을떄 -> 저장된 패턴 리스트들을 삭제한다
    public void deletePattern()
    {
        foreach(var pattern in patternList)
        {
            Destroy(pattern);
        }
        patternList.Clear();
    }

    // 복잡.. Pattern Object의 정보들을 Json으로 변경한다
    public void SavePattern()
    {
        // 저장할 패턴 생성
        PatternData pattern = new PatternData();
        float endPos_x = patternList[patternList.Count - 1].transform.position.x;

        for(int i = 0; i < patternList.Count; i++)
        {
            // Platform인 경우
            if (patternList[i].name == "Platform")
            {
                int platformType = typeList[i]; // 플랫폼 타입 가져옴
                float platformPos = patternList[i].transform.position.x - startPos_x; // 위치 정보 저장
                PatternData.PlatformData platformData = new PatternData.PlatformData();
                platformData.type = platformType;
                platformData.pos = platformPos;
                pattern.p_Data.Add(platformData);
            }
            // Enemy인 경우
            else if (patternList[i].name == "Enemy")
            {
                // 오브젝트 정보를 통해 
                int enemyType = typeList[i];
                int enemyFloor;
                if (floorList[i] == 2) enemyFloor = 2; // 3층, 날몹
                else if (floorList[i] == 1) enemyFloor = 1;
                else enemyFloor = 0;
                float enemyPos = patternList[i].transform.position.x - startPos_x;

                PatternData.EnemyData enemyData = new PatternData.EnemyData();
                enemyData.type = enemyType;
                enemyData.floor = enemyFloor;
                enemyData.x_pos = enemyPos;
                pattern.e_Data.Add(enemyData);
            }
        }
        pattern.patternTime = (endPos_x - startPos_x) / 2;
        Debug.Log($"패턴타임 : {pattern.patternTime}초");
        Debug.Log("저장하겠습니다");
        // DataManager의 SavePatternData 통해서 Pattern 저장
        this.GetComponent<DataManager>().SavePatternData(pattern);
    }

    // UI
    public void IncStage() { nowStage++; stageText.text = $"Stage : {nowStage}"; }
    public void DecStage() { nowStage--; stageText.text = $"Stage : {nowStage}"; }
    public void IncPhase() { nowPhase++; phaseText.text = $"Phase : {nowPhase}"; }
    public void DecPhase() { nowPhase--; phaseText.text = $"Phase : {nowPhase}"; }
    public void IncPattern() { nowPattern++; patternText.text = $"Pattern : {nowPattern}"; }
    public void DecPattern() { nowPattern--; patternText.text = $"Pattern : {nowPattern}"; }
}
