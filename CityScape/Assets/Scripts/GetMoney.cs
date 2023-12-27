using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetMoney : MonoBehaviour
{
    public GameData gameData;
    public static int getMoney; //얻은 돈(누적x)

    private TextMeshProUGUI textComponent;

    //싱글톤 적용
    public static GetMoney instance;
    
    private void Awake(){
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {  
        getMoney = 0; //초기화
        gameData = DataManager.instance.GetGameData();
        textComponent = GetComponent<TextMeshProUGUI>();
        gameData.money += getMoney;
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = "X " + getMoney.ToString();
    }
}
