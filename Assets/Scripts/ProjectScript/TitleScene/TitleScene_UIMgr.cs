using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene_UIMgr : MonoBehaviour
{
    public GameObject soundObj;

    void Awake()
    {
/*
        //������Ϸ���Ƚ���˵��������򿪲˵�panel
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "SoundEffect";
        }

*/
        //������Ϸ���Ƚ���˵��������򿪲˵�panel
        UIManager.GetInstance().ShowPanel<UI_TitleScenePanel>("UI_TitleScene", E_UI_Layer.Bot);
        PlayMenuBGM();
    }

    public void PlayMenuBGM()
    {
        //�����������BGM
        MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano41-�˵�");

        MusicMgr.GetInstance().ChangeBKValue(0.5f);
    }
}
