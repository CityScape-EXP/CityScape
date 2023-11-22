using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatternManager : MonoBehaviour
{
    // 편하게 사용할 수 있게 자체참조
    public static PatternManager instance;

    // 패턴리스트 -> Draw된 패턴들의 GameObject 정보를 저장한다
    private List<GameObject> patternList;
    // 타입리스트 -> 패턴들이 Draw된 타입을 저장한다.
    // 플랫폼 - 0:긴플랫폼 1:짧은플랫폼
    private List<int> typeList;

    // 코드가 짧아서 UI도 같이 넣어버림
    public GameObject stageTextObj, phaseTextObj, patternTextObj;
    TMP_Text stageText, phaseText, patternText;
    int nowStage = 0; int nowPhase = 0; int nowPattern = 0;

    private void Start()
    {
        instance = this;
        stageText = stageTextObj.GetComponent<TMP_Text>();
        phaseText = phaseTextObj.GetComponent<TMP_Text>();
        patternText = patternTextObj.GetComponent<TMP_Text>();
        patternList = new List<GameObject>();
    }

    public void NewPattern(GameObject pattern, int type) 
    { 
        patternList.Add(pattern); 
        typeList.Add(type);
    }

    public void deletePattern()
    {
        foreach(var pattern in patternList)
        {
            Destroy(pattern);
        }
        patternList.Clear();
    }

    private void SavePattern()
    {
        // 저장할 패턴 생성
        PatternData pattern = new PatternData();
        // 첫번째 오브젝트의 x좌표를 0으로 설정한다
        float startPos_x = patternList[0].transform.position.x;

        for(int i = 0; i < patternList.Count; i++)
        {
            // Platform인 경우
            if (patternList[i].name == "Platform")
            {
                int platformType = typeList[i]; // 플랫폼 타입 가져옴
                //float obj_xpos = // 위치 정보 저장
            }
            // Enemy인 경우
            else if (patternList[i].name == "Enemy")
            {
                int enemyType = typeList[i];
            }
        }

        // PatternData정보 Json으로 변환 후 저장
    }

    // UI
    public void IncStage() { nowStage++; stageText.text = $"Stage : {nowStage}"; }
    public void DecStage() { nowStage--; stageText.text = $"Stage : {nowStage}"; }
    public void IncPhase() { nowPhase++; phaseText.text = $"Phase : {nowPhase}"; }
    public void DecPhase() { nowPhase--; phaseText.text = $"Phase : {nowPhase}"; }
    public void IncPattern() { nowPattern++; patternText.text = $"Pattern : {nowPattern}"; }
    public void DecPattern() { nowPattern--; patternText.text = $"Pattern : {nowPattern}"; }
}
