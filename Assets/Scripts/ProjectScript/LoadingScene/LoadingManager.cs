using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;
    public TMP_Text progressText;

    private string currentTargetSceneName;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<float>("进度条更新", UpdateProgressBar);

        currentTargetSceneName = GameDataControl.GetInstance().PlayerDataInfo.currentTargetScene;
    }


    private void Start()
    {
        ScenesMgr.GetInstance().LoadSceneAsyn(currentTargetSceneName, loadScene);
    }

    private void loadScene()
    {
        if(currentTargetSceneName == "MainPage")
        {
            if (GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 0)
            {
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
            }

            if(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 6)
            {
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
            }

            EventCenter.GetInstance().EventTrigger("turnOffBK");
        }

    }

    private void UpdateProgressBar(float progress)
    {
        progressBar.value = progress;
        progressText.text = "读取进度: " + (float)(progress * 100) + "%";
    }
}
