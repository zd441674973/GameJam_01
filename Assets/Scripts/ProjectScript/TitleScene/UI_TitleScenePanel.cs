using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScenePanel : BasePanel
{

    //��ҵĴ洢·��
    private static string PlayerInfoSaveAdress;

    protected override void Awake()
    {
        base.Awake();

        PlayerInfoSaveAdress = Application.persistentDataPath + "/PlayerSaveData.json";

        GetControl<Button>("Button_NewGame").onClick.AddListener(NewGame);
        GetControl<Button>("Button_ContinueGame").onClick.AddListener(ContinueGame);
        GetControl<Button>("Button_Setting").onClick.AddListener(SettingGame);
        GetControl<Button>("Button_ExitGame").onClick.AddListener(ExitGame);
    }

    private void NewGame()
    {
        MusicMgr.GetInstance().PlaySound("maou_se_system48-start", false);
        //��ʼ����Ϸ֮ǰ��ɾ���ɴ浵������
        if (File.Exists(PlayerInfoSaveAdress))
        {
            File.Delete(PlayerInfoSaveAdress);
            GameDataControl.GetInstance().PlayerDataInfo = new PlayerInfo();
        }

        ScenesMgr.GetInstance().LoadSceneAsyn("MainPage", loadScene);
    }

    private void ContinueGame()
    {
        //��ȡ����
        GameDataControl.GetInstance().LoadPlayerInfo();

        ScenesMgr.GetInstance().LoadSceneAsyn("MainPage", loadScene);
    }

    private void SettingGame()
    {
        UIManager.GetInstance().ShowPanel<UI_SettingPanel>("UI_SettingPanel", E_UI_Layer.Top);
    }


    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private void loadScene()
    {
        UIManager.GetInstance().HidePanel("UI_TitleScene");

        if(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 0)
        {
            UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
        }
    }
}
