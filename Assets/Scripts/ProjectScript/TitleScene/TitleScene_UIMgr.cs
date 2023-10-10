using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene_UIMgr : MonoBehaviour
{
    public GameObject soundObj;

    private bool TitlePanleIsOpen; 

    void Awake()
    {
        TitlePanleIsOpen = false;


        EventCenter.GetInstance().AddEventListener("TitlePanelIsOpen",()=> TitlePanleIsOpen = true);
            
            
            /*
                //进入游戏首先进入菜单场景并打开菜单panel
                if (soundObj == null)
                {
                    soundObj = new GameObject();
                    soundObj.name = "SoundEffect";
                }

        */
        // 查找Canvas

    }

    private void Start()
    {
        if (TitlePanleIsOpen)
        {

        }
        else
        {        //进入游戏首先进入菜单场景并打开菜单panel
            UIManager.GetInstance().ShowPanel<UI_TitleScenePanel>("UI_TitleScene", E_UI_Layer.Bot);

        }


        PlayMenuBGM();
    }

    public void PlayMenuBGM()
    {
        //播放主界面的BGM
        MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano41-菜单");
    }
}
