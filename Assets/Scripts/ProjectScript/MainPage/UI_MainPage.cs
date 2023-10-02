using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainPage : BasePanel
{
    public bool MenuIsOpen = false;
    public bool CardLabraryIsOpen = false;
    public TMP_Text playerinfo;


    public GameObject CardLabPlus;

    public override void ShowMe()
    {
        base.ShowMe();
        //���»�����Ϣ
        UpdateInfo();
        GetControl<Button>("OpenMenu").onClick.AddListener(OpenMenu);
        GetControl<Button>("CardLibrary").onClick.AddListener(OpenCardLabrary);

        EventCenter.GetInstance().AddEventListener("CloseMenu", ChangeMenuState);
    }

    protected override void Awake()
    {
        //�����awake����ʼ����Ϣ
        base.Awake();
        MenuIsOpen = false;
        CardLabraryIsOpen = false;

        EventCenter.GetInstance().AddEventListener("currentPlayerNodeIDchange", UpdateInfo);
        EventCenter.GetInstance().AddEventListener("CardPlusOne", showCardPlus1Icon);
    }

    
    public override void HideMe()
    {
        base.HideMe();
        //�ر����ʱ��������
        GameDataControl.GetInstance().SavePlayerInfo();

        EventCenter.GetInstance().RemoveEventListener("CloseMenu", ChangeMenuState);
        EventCenter.GetInstance().RemoveEventListener("currentPlayerNodeIDchange", UpdateInfo);
        EventCenter.GetInstance().RemoveEventListener("CardPlusOne", showCardPlus1Icon);
    }

    private void showCardPlus1Icon()
    {
        StartCoroutine(ActivateCardPlus());
    }

    private IEnumerator ActivateCardPlus()
    {
        // ��������
        CardLabPlus.SetActive(true);

        // �ȴ�1��
        yield return new WaitForSeconds(1f);

        // ʧ������
        CardLabPlus.SetActive(false);
    }


    private void UpdateInfo()
    {
        playerinfo.text = "����" + "  �׶�: " + GameDataControl.GetInstance().PlayerDataInfo.currentNodeID + "/5" + "  �������ֵ" + GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
    }


    public void ChangeMenuState()
    {
        MenuIsOpen = false;
    }

    public void OpenMenu()
    {
        if (!MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);
            UIManager.GetInstance().ShowPanel<UI_MenuPanel>("UI_MenuPanel", E_UI_Layer.Top);
            MenuIsOpen = true;
        }

        if (MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);
            UIManager.GetInstance().HidePanel("UI_MenuPanel");
            MenuIsOpen = false;
        }
    }

    public void OpenCardLabrary()
    {
        if (!CardLabraryIsOpen)
        {

            MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);

            UIManager.GetInstance().ShowPanel<UI_CardLibrary>("UI_CardLibrary", E_UI_Layer.Mid);
            CardLabraryIsOpen = true;
        }

        if (CardLabraryIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);

            UIManager.GetInstance().HidePanel("UI_CardLibrary");
            CardLabraryIsOpen = false;
        }
    }


    //ִ���л���������Ҫִ�еĺ���
    public void AfterLoadFunctions()
    {
        MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);
        UIManager.GetInstance().HidePanel("UI_MainPage");
        MenuIsOpen = false;
    }
}
