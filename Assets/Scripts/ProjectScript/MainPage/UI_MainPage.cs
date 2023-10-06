using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainPage : BasePanel
{
    public bool MenuIsOpen = false;
    public bool CardLabraryIsOpen = false;
    public TMP_Text playerinfo;
    public Image UIArea;

    public GameObject CardLabPlus;

    private Button openMenuButton;
    private Button CardLabraryButton;

    private Quaternion originalRotation;
    private bool isRotating = false;

    public override void ShowMe()
    {
        base.ShowMe();
        //���»�����Ϣ
        UpdateInfo();
        GetControl<Button>("OpenMenu").onClick.AddListener(OpenMenu);
        GetControl<Button>("CardLibrary").onClick.AddListener(OpenCardLabrary);

        EventCenter.GetInstance().AddEventListener("CloseMenu", ChangeMenuState);
        EventCenter.GetInstance().AddEventListener("CloseCardLibrary", ChangeCardlabState);
    }

    protected override void Awake()
    {
        //�����awake����ʼ����Ϣ
        base.Awake();
        MenuIsOpen = false;
        CardLabraryIsOpen = false;

        MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano09-��ʼ");

        EventCenter.GetInstance().AddEventListener("currentPlayerNodeIDchange", UpdateInfo);
        EventCenter.GetInstance().AddEventListener("CardPlusOne", showCardPlus1Icon);

        openMenuButton = GetControl<Button>("OpenMenu");
        CardLabraryButton = GetControl<Button>("CardLibrary");

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //����������������Ƴ����¼������д���
        EventTrigger trigger = openMenuButton.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(MouseEnterOpenMenuButton);

        trigger.triggers.Add(enter);

        //����������������Ƴ����¼������д���
        EventTrigger trigger2 = CardLabraryButton.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter2 = new EventTrigger.Entry();
        enter2.eventID = EventTriggerType.PointerEnter;
        enter2.callback.AddListener(MouseEnterOpenCardLabraryButton);

        trigger2.triggers.Add(enter2);
    }


    public override void HideMe()
    {
        base.HideMe();
        //�ر����ʱ��������
        GameDataControl.GetInstance().SavePlayerInfo();

        EventCenter.GetInstance().RemoveEventListener("CloseMenu", ChangeMenuState);
        EventCenter.GetInstance().RemoveEventListener("currentPlayerNodeIDchange", UpdateInfo);
        EventCenter.GetInstance().RemoveEventListener("CardPlusOne", showCardPlus1Icon);
        EventCenter.GetInstance().RemoveEventListener("CloseCardLibrary", ChangeCardlabState);
    }



    public void MouseEnterOpenMenuButton(BaseEventData data)
    {
        originalRotation = openMenuButton.transform.rotation;

        if (!isRotating)
        {
            //StartCoroutine(RotateButton(openMenuButton));
        }
    }

    public void MouseEnterOpenCardLabraryButton(BaseEventData data)
    {
        originalRotation = CardLabraryButton.transform.rotation;

        if (!isRotating)
        {
            //StartCoroutine(RotateButton(CardLabraryButton));
        }
    }

/*
    private IEnumerator RotateButton(Button button)
    {
        isRotating = true;

        float rotationDuration = 0.05f;
        float targetRotationAngle = -15f;
        float elapsedTime = 0f;

        Quaternion startRotation = button.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetRotationAngle);

        while (elapsedTime < rotationDuration)
        {
            button.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        button.transform.rotation = targetRotation;

        // �ȴ�һ��ʱ���ԭ
        yield return new WaitForSeconds(0.1f);

        elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            button.transform.rotation = Quaternion.Slerp(targetRotation, startRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        button.transform.rotation = startRotation;
        isRotating = false;
    }*/



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
        playerinfo.text = "����" + "  ����ɽ׶�: " + GameDataControl.GetInstance().PlayerDataInfo.currentNodeID + "/5" + "  �������ֵ" + GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
    }


    public void ChangeMenuState()
    {
        MenuIsOpen = false;
    }

    public void ChangeCardlabState()
    {
        CardLabraryIsOpen = false;
    }

    public void OpenMenu()
    {
        if (!MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

            UIManager.GetInstance().ShowPanel<UI_MenuPanel>("UI_MenuPanel", E_UI_Layer.Top);
            MenuIsOpen = true;
        }

        if (MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

            UIManager.GetInstance().HidePanel("UI_MenuPanel");
            MenuIsOpen = false;
        }
    }

    public void OpenCardLabrary()
    {
        if (!CardLabraryIsOpen)
        {

            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

            UIManager.GetInstance().ShowPanel<UI_CardLibrary>("UI_CardLibrary", E_UI_Layer.Mid);
            CardLabraryIsOpen = true;
        }

        if (CardLabraryIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

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
