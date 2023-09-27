using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    void Awake()
    {
        //������Ϸ���Ƚ���˵��������򿪲˵�panel
        UIManager.GetInstance().ShowPanel<UI_MainPage>("UI_MainPage", E_UI_Layer.Bot);
        PlayMenuBGM();
    }

    public void PlayMenuBGM()
    {
        //�����������BGM
        MusicMgr.GetInstance().PlayBkMusic("Black");
        MusicMgr.GetInstance().ChangeBKValue(0.3f);
    }
}
