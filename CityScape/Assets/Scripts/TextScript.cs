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
    public static int score = 0;
    public static int bestScore = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        _text.text = HP.ToString();
    }

    public void HpMinus(){
        if(HP == 1) ShowPopup();
        else{
            HP -= 1;
            _text.text = HP.ToString();
        }
    }
    void ShowPopup()
    {

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
