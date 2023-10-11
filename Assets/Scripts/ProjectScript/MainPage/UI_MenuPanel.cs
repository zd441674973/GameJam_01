using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuPanel : BasePanel
{
    public override void ShowMe()
    {
        base.ShowMe();
        EventCenter.GetInstance().EventTrigger("MenuIsOpen");
    }

    public override void HideMe()
    {
        base.HideMe();
        EventCenter.GetInstance().EventTrigger("MenuIsClose");
    }

    protected override void Awake()
    {
        base.Awake();
        //激活时发送事件，Menu已经打开

        GetControl<Button>("ReturnToTitle").onClick.AddListener(ReturnToTitle);
        GetControl<Button>("ExitGame").onClick.AddListener(ExitGame);
        GetControl<Button>("SaveGame").onClick.AddListener(SaveGame);
        GetControl<Button>("Setting").onClick.AddListener(Setting);
        GetControl<Button>("CloseMenu").onClick.AddListener(CloseMenu);
    }

    public void ReturnToTitle()
    {
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
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        UIManager.GetInstance().HidePanel("UI_MenuPanel");
        //触发事件监听，保存数据
        EventCenter.GetInstance().EventTrigger("SavePlayerInfo");
    }
    public void Setting()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        UIManager.GetInstance().HidePanel("UI_MenuPanel");
        UIManager.GetInstance().ShowPanel<UI_SettingPanel>("UI_SettingPanel", E_UI_Layer.Top);
    }
    public void CloseMenu()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        EventCenter.GetInstance().EventTrigger("CloseMenu");
        UIManager.GetInstance().HidePanel("UI_MenuPanel");
    }

    public void AfterReturnToTitle()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        UIManager.GetInstance().HidePanel("UI_MenuPanel");
        UIManager.GetInstance().HidePanel("UI_MainPage");
        UIManager.GetInstance().HidePanel("UI_CardLibrary");
        UIManager.GetInstance().HidePanel("UI_GameMap");
        UIManager.GetInstance().HidePanel("AwardPanel");
        UIManager.GetInstance().HidePanel("DelateCardPanel");
        UIManager.GetInstance().HidePanel("UI_DialoguePanel");
        //每多加一个面板就在这里加入一个，方便关闭

    }
}
