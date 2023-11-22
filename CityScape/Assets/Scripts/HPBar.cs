using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image[] hpPointers;
    [SerializeField] private Image[] upHpBars;
    private int hpUpgrade;
    private void Start()
    {
        hpUpgrade = Player.instance.playerHp - 3;
        for (int i = 4; i>hpUpgrade; i--)
        {
            upHpBars[i-1].enabled = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < hpPointers.Length; i++)
        {
            hpPointers[i].enabled = !DisplayHp(Player.instance.playerCurrentHp, i);
        }
    }
    bool DisplayHp(float hp,int pointnum)
    {
        return ((pointnum) >= hp);
    }
}
