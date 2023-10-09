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
    public TMP_Text playerHP;
    public Image UIArea;

    public GameObject CardLabPlus;

    private Button openMenuButton;
    private Button CardLabraryButton;

    private bool isMenuButtonRotating = false;
    public Image MenuButtonIcon;

    private bool isCardLabButtonRotating = false;
    public Image CadLabButtonIcon;


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

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(MouseExitOpenMenuButton);

        trigger.triggers.Add(enter);
        trigger.triggers.Add(exit);

        //����������������Ƴ����¼������д���
        EventTrigger trigger2 = CardLabraryButton.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter2 = new EventTrigger.Entry();
        enter2.eventID = EventTriggerType.PointerEnter;
        enter2.callback.AddListener(MouseEnterOpenCardLabraryButton);

        EventTrigger.Entry exit2 = new EventTrigger.Entry();
        exit2.eventID = EventTriggerType.PointerExit;
        exit2.callback.AddListener(MouseExitOpenCardLabraryButton);

        trigger2.triggers.Add(enter2);
        trigger2.triggers.Add(exit2);
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

    private void Update()
    {
        ChnageButtonIcon();

        Debug.Log(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID);
    }

    private void MouseEnterOpenMenuButton(BaseEventData data)
    {
        isMenuButtonRotating = true;
    }

    private void MouseExitOpenMenuButton(BaseEventData data)
    {
        isMenuButtonRotating = false;
    }

    public void MouseEnterOpenCardLabraryButton(BaseEventData data)
    {
        isCardLabButtonRotating = true;
    }

    public void MouseExitOpenCardLabraryButton(BaseEventData data)
    {
        isCardLabButtonRotating = false;
    }

    private void ChnageButtonIcon()
    {
        if (isMenuButtonRotating)
        {
            MenuButtonIcon.transform.Rotate(Vector3.forward * -100 * Time.deltaTime);
        }

        if (isCardLabButtonRotating || CardLabraryIsOpen)
        {
            CadLabButtonIcon.sprite = ResMgr.GetInstance().Load<Sprite>("UISprites/�ƿ�Icon����");
        }
        else
        {
            CadLabButtonIcon.sprite = ResMgr.GetInstance().Load<Sprite>("UISprites/�ƿ�Icon");
        }
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
        playerinfo.text = "����" + "  ����ɽ׶�: " + GameDataControl.GetInstance().PlayerDataInfo.currentNodeID + "/5";
        playerHP.text = "�������ֵ: " + GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
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
