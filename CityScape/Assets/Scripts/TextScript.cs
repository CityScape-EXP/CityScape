using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    [SerializeField] 
    Text _text;

    public GameObject Popup;

    public float HP = 0;
    public float Damage = 0;
    public static int score = 0;
    public static int bestScore = 0;
    private GameObject target;

    private float Attack
    {
        get { return HP - Damage; }
    }

    void Awake(){
        target = GameObject.FindWithTag("Popup");

        if(target == null){
            Debug.LogError("'Popup'태그를 찾을 수 없음.");
        }
        else HpMinus();
    }

    // Start is called before the first frame update
    void Start()
    {
        _text.text = HP.ToString();
    }

    public void HpMinus(){
        if(Attack == 0) ShowPopup();
        else{
            HP -= Damage;
            _text.text = HP.ToString();
        }
    }

    void ShowPopup()
    {

        Popup.SetActive(true);
        /*
    //Get the canvas RectTransform to calculate the center

    void ShowPopup(){ 
        Popup.SetActive(true);
    /*
    // Get the canvas RectTransform to calculate the center

    Canvas canvas = GetComponentInParent<Canvas>();
    
    if (canvas == null){
        Debug.LogError("Canvas component not found in parent.");
        return;
    }

    RectTransform canvasRect = canvas.GetComponent<RectTransform>();

    if (canvasRect == null){
        Debug.LogError("RectTransform component not found in the Canvas.");
        return;
    }

    // Calculate the center of the canvas
    Vector3 centerOfCanvas = new Vector3(canvasRect.rect.width / 2, canvasRect.rect.height / 2, 0);

    // Instantiate the Popup at the center of the canvas
    Instantiate(Popup, centerOfCanvas, Quaternion.identity);
        */
    }
}

