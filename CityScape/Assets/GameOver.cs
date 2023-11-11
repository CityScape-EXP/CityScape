using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject Popup;

    // Update is called once per frame
    public void PlayerDie()
    {
        if(Score.score == 1){
            Instantiate(Popup, transform.position, Quaternion.identity);
        }
    }
}