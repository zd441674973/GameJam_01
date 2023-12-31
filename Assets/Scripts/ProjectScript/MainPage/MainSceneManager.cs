using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    void Awake()
    {
        //进入游戏首先进入菜单场景并打开菜单panel
        UIManager.GetInstance().ShowPanel<UI_MainPage>("UI_MainPage", E_UI_Layer.Bot);
        UIManager.GetInstance().ShowPanel<UI_GameMap>("UI_GameMap", E_UI_Layer.Mid);

        

        PlayMenuBGM();

        if (!GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard)
        {
            UIManager.GetInstance().ShowPanel<UI_AwardPanel>("AwardPanel", E_UI_Layer.Mid);
        }

        if (!GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_DelateCard)
        {
            UIManager.GetInstance().ShowPanel<UI_DelateCardPanel>("DelateCardPanel", E_UI_Layer.Mid);
        }
    }

    public void PlayMenuBGM()
    {
        //播放主界面的BGM
        MusicMgr.GetInstance().PlayBkMusic("Black");
        MusicMgr.GetInstance().ChangeBKValue(0.3f);
    }
}
