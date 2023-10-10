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
                //������Ϸ���Ƚ���˵��������򿪲˵�panel
                if (soundObj == null)
                {
                    soundObj = new GameObject();
                    soundObj.name = "SoundEffect";
                }

        */
        // ����Canvas

    }

    private void Start()
    {
        if (TitlePanleIsOpen)
        {

        }
        else
        {        //������Ϸ���Ƚ���˵��������򿪲˵�panel
            UIManager.GetInstance().ShowPanel<UI_TitleScenePanel>("UI_TitleScene", E_UI_Layer.Bot);

        }


        PlayMenuBGM();
    }

    public void PlayMenuBGM()
    {
        //�����������BGM
        MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano41-�˵�");
    }
}
