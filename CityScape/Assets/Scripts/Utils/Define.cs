using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class Define
{

    /// <summary>
    /// UI Event 종류 지정
    /// </summary>
    public enum UIEvent
    {
        Click,
        Drag,
        DragEnd,
    }


    public enum Sounds
    {
        BGM,
        SFX,
        MaxCount
    }
    public enum BGM
    {
        main_BGM,
        St_1,
        St_2,
        St_3,


        MaxCount
    }
    public enum SFX
    {
        Chac_roll_1,
        Char_death_1,
        Char_gunfire_1,
        Char_hit_1,
        Char_jump_1,
        Char_land_1,
        Enemy_death_1,
        Enemy_gunfire_1,
        Enemy_hit_1,
        UI_select_1,
        UI_title_1,
        UI_touch_1,
        UI_upgrade_1,
        UI_upgrade_fail_1,


        MaxCount
    }


    public enum Scene
    {
        Game,
    }

    public enum UI_Type
    {
        MainMenuUI,
        ReinforcementUI,
        TitleUI,
        CreditUI,
        SettingUI,
        PauseUI1,
        PauseUI2,
        PauseUI3,
        GameOverUI,
        ClearUI,
    }


    public enum Stages 
    { 
        Stage1, 
        Stage2,
        Stage3
    }

    public enum Reinforcement
    {
        AttackSpeed,
        Health,
        Power,
    }


    public enum MonsterType 
    {
        NorEnemy,
        ReinEnemy,
        FlyEnemy,
    }


}