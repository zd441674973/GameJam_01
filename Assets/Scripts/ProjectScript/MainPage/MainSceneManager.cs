using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    void Awake()
    {
        //进入游戏首先进入菜单场景并打开菜单panel
        UIManager.GetInstance().ShowPanel<UI_MainPage>("UI_MainPage", E_UI_Layer.Bot);
        PlayMenuBGM();
    }

    public void PlayMenuBGM()
    {
        //播放主界面的BGM
        MusicMgr.GetInstance().PlayBkMusic("Black");
        MusicMgr.GetInstance().ChangeBKValue(0.3f);
    }
}
