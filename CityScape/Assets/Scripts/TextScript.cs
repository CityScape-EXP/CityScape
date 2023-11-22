using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{
    [SerializeField] 
    Text _text;

    public GameObject popupGameOver;
    GameObject target1;

    //초기설정
    private float HP = 100f;
    private float Damage = 10f;

    private float Attack => HP - Damage;

    void Awake(){
        target1 = GameObject.FindWithTag("popupGameOver");

        if(target1 == null){
            Debug.Log("'Popup'태그를 찾을 수 없음.");
        }
        else HpMinus();
    }

    // Start is called before the first frame update
    void Start()
    {
        _text.text = HP.ToString();
    }

    public void HpMinus(){
        if(IsDead()) {
            if (Score.score > Score.highScore)
            {
                Score.highScore = Score.score;
            }
            ShowPopup();
        }
        else{
            HP -= Damage;
            _text.text = HP.ToString();
        }
    }

    private bool IsDead(){
        return Attack <= 0;
    }

    void ShowPopup()
    {
        popupGameOver.SetActive(true);
    }
}

