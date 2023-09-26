using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene_UIMgr : MonoBehaviour
{
    void Awake()
    {
        //������Ϸ���Ƚ���˵��������򿪲˵�panel
        UIManager.GetInstance().ShowPanel<UI_TitleScenePanel>("UI_TitleScene", E_UI_Layer.Bot);
        PlayMenuBGM();
    }

    public void PlayMenuBGM()
    {
        //�����������BGM
        MusicMgr.GetInstance().PlayBkMusic("Black");
        MusicMgr.GetInstance().ChangeBKValue(0.3f);
    }
}
