using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScenePanel : BasePanel
{

    //玩家的存储路径
    private static string PlayerInfoSaveAdress;

    protected override void Awake()
    {
        base.Awake();

        PlayerInfoSaveAdress = Application.persistentDataPath + "/PlayerSaveData.json";

        GetControl<Button>("Button_NewGame").onClick.AddListener(NewGame);
        GetControl<Button>("Button_ContinueGame").onClick.AddListener(ContinueGame);
        GetControl<Button>("Button_ExitGame").onClick.AddListener(ExitGame);
    }

    private void NewGame()
    {
        MusicMgr.GetInstance().PlaySound("maou_se_system48-start", false);
        //开始新游戏之前先删除旧存档的数据
        if (File.Exists(PlayerInfoSaveAdress))
        {
            File.Delete(PlayerInfoSaveAdress);
            GameDataControl.GetInstance().PlayerDataInfo = new PlayerInfo();
        }

        ScenesMgr.GetInstance().LoadSceneAsyn("MainPage", loadScene);
    }

    private void ContinueGame()
    {
        //读取数据
        GameDataControl.GetInstance().LoadPlayerInfo();
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
    }
}
