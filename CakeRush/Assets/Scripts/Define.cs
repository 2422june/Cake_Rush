using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum GameSound
    {
        FX_All_ClickedButton,
        BGM_Lobby_Main,
        BGM_Lobby_GameMatching,
        FX_Lobby_GameStart,

        BGM_Game_Main,
        BGM_Game_Timeless,

        FX_Player_Attack,
        FX_Player_CakeRush,
        FX_Player_CokeShot,
        FX_Player_Lightning,
        FX_Player_ShootingStar,

        FX_Build_BuildSuccess,

        FX_Unit_Attack,
        FX_CokeTower_Awake,
        FX_CokeTower_Attack,
        FX_CokeTower_Hit,

        
        FX_Game_Win,
        FX_Game_Loss,
        BGM_Title_Main,
    }

    public enum Scene
    {
        noting, title, lobby, inGame, victory, defeat
    };
}
