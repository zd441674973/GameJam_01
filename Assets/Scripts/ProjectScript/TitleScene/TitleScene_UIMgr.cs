using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene_UIMgr : MonoBehaviour
{
    public GameObject soundObj;

    void Awake()
    {
/*
        //进入游戏首先进入菜单场景并打开菜单panel
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "SoundEffect";
        }

*/
        //进入游戏首先进入菜单场景并打开菜单panel
        UIManager.GetInstance().ShowPanel<UI_TitleScenePanel>("UI_TitleScene", E_UI_Layer.Bot);
        PlayMenuBGM();
    }

    public void PlayMenuBGM()
    {
        //播放主界面的BGM
        MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano41-菜单");

        MusicMgr.GetInstance().ChangeBKValue(0.5f);
    }
}
