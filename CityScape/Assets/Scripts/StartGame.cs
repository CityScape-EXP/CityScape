using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject mainPopup;

    public void GameStart(){
        Instantiate(mainPopup, transform.position, Quaternion.identity);
    }
}
