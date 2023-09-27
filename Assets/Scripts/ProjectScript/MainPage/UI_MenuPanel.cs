using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
        //����ʱ�����¼���Menu�Ѿ���

        GetControl<Button>("ReturnToTitle").onClick.AddListener(ReturnToTitle);
        GetControl<Button>("ExitGame").onClick.AddListener(ExitGame);
        GetControl<Button>("SaveGame").onClick.AddListener(SaveGame);
        GetControl<Button>("Setting").onClick.AddListener(Setting);
        GetControl<Button>("CloseMenu").onClick.AddListener(CloseMenu);
    }

    public void ReturnToTitle()
    {
        MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);
        ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void SaveGame()
    {
        MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);
        UIManager.GetInstance().HidePanel("UI_MenuPanel");
        //�����¼���������������
        EventCenter.GetInstance().EventTrigger("SavePlayerInfo");
    }
    public void Setting()
    {
        MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);
    }
    public void CloseMenu()
    {
        MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);

        EventCenter.GetInstance().EventTrigger("CloseMenu");
        UIManager.GetInstance().HidePanel("UI_MenuPanel");
    }

    public void AfterReturnToTitle()
    {

        MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);
        UIManager.GetInstance().HidePanel("UI_MenuPanel");
        UIManager.GetInstance().HidePanel("UI_MainPage");
        //ÿ���һ���������������һ��������ر�

    }
}