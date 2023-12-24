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


        MaxCount
    }
    public enum SFX
    {
        Chac_roll_1128,
        Char_death_1128,
        Char_gunfire_1128,
        Char_hit_1128,
        Char_jump_1128,
        Char_land_1128,
        Enemy_death_1_1128,
        Enemy_death_2_1128,
        Enemy_gunfire_1128,
        Enemy_hit_1128,
        UI_select_1128,
        UI_title_1128,
        UI_touch_1128,
        UI_upgrade_1128,
        UI_upgrade_fail_1128,


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
        PauseUI,
    }


}