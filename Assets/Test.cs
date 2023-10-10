using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public HealthSystem enemyhealthSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Function();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShowAwardPanel();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AdddrawNewCardTimes();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameDataControl.GetInstance().PlayerDataInfo.currentNodeID += 1;
            ShowDialogue();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            EventCenter.GetInstance().EventTrigger("ScreenShake");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            enemyhealthSystem.Health = 0;
        }

        Debug.Log(GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard);

        Debug.Log(GameDataControl.GetInstance().PlayerDataInfo.drawNewCardTimes);

        if (Input.GetKeyDown(KeyCode.X))
        {

            UIManager.GetInstance().HidePanel("UI_TitleScene");
        }
    }

    void Function()
    {
        //GameDataControl.GetInstance().PlayerDataInfo.currentNodeID += 1;

        //EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");
    }

    void ShowAwardPanel()
    {
        UIManager.GetInstance().ShowPanel<UI_AwardPanel>("AwardPanel");
    }

    private void AdddrawNewCardTimes()
    {
        GameDataControl.GetInstance().PlayerDataInfo.drawNewCardTimes += 3;
    }

    private void ShowDialogue()
    {
        UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
    }

}
