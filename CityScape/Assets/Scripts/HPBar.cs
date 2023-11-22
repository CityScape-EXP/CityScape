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
        hpUpgrade = Player.instance.playerHp - 3; // hpUpgrade 변수를 내 hp 업그레이드 횟수와 같게 초기화
        for (int i = 4; i>hpUpgrade; i--) // 업그레이드에 따라 체력바의 길이를 조절하는 for문
        {
            upHpBars[i-1].enabled = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < hpPointers.Length; i++) // 캐릭터 피격시 hp칸 감소시키게 하는 for문
        {
            hpPointers[i].enabled = !DisplayHp(Player.instance.playerCurrentHp, i);
        }
    }
    bool DisplayHp(float hp,int pointnum) // hp가 pointnum보다 작으면 false, 크면 true를 리턴
    {
        return ((pointnum) >= hp);
    }
}
