using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_AwardPanel : BasePanel
{
    public UI_CardDetail NewcardSlot1;
    public UI_CardDetail NewcardSlot2;
    public UI_CardDetail NewcardSlot3;
    public Image NewcardSlot1Cover1;
    public Image NewcardSlot2Cover2;
    public Image NewcardSlot3Cover3;
    private int currentSelectedNewCard;


    private List<Card> allCards;
    private List<Card> drawnNewCards;
    public float rareCardPosibility = 0.7f;

    public override void ShowMe()
    {
        base.ShowMe();
    }

    protected override void Awake()
    {
        base.Awake();
        drawnNewCards = new List<Card>();
        GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard = false;

        GetControl<Button>("ButtonNextStep").onClick.AddListener(SkipSelectCard);

        //����������������Ƴ����¼������д���
        EventTrigger trigger1 = NewcardSlot1Cover1.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter1 = new EventTrigger.Entry();
        enter1.eventID = EventTriggerType.PointerEnter;
        enter1.callback.AddListener(EnterNewCardSlot1);
        //����һ������Ƴ����¼������
        EventTrigger.Entry exit1 = new EventTrigger.Entry();
        exit1.eventID = EventTriggerType.PointerExit;
        exit1.callback.AddListener(ExitNewCardSlot1);

        trigger1.triggers.Add(enter1);
        trigger1.triggers.Add(exit1); 

        ////////////////////////////////////////////////////////////////////////////////////
        EventTrigger trigger2 = NewcardSlot2Cover2.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter2 = new EventTrigger.Entry();
        enter2.eventID = EventTriggerType.PointerEnter;
        enter2.callback.AddListener(EnterNewCardSlot2);
        //����һ������Ƴ����¼������
        EventTrigger.Entry exit2 = new EventTrigger.Entry();
        exit2.eventID = EventTriggerType.PointerExit;
        exit2.callback.AddListener(ExitNewCardSlot2);

        trigger2.triggers.Add(enter2);
        trigger2.triggers.Add(exit2);

        ////////////////////////////////////////////////////////////////////////////////////
        EventTrigger trigger3 = NewcardSlot3Cover3.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter3 = new EventTrigger.Entry();
        enter3.eventID = EventTriggerType.PointerEnter;
        enter3.callback.AddListener(EnterNewCardSlot3);
        //����һ������Ƴ����¼������
        EventTrigger.Entry exit3 = new EventTrigger.Entry();
        exit3.eventID = EventTriggerType.PointerExit;
        exit3.callback.AddListener(ExitNewCardSlot3);

        trigger3.triggers.Add(enter3);
        trigger3.triggers.Add(exit3);
        ////////////////////////////////////////////////////////////////////////////////////

        //��ʾ�¿���
        RefreshNewCards();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //��������
            SelectCard();
        }
    }

    private void SkipSelectCard()
    {
        UIManager.GetInstance().HidePanel("AwardPanel");
        GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard = true;

        UIManager.GetInstance().ShowPanel<UI_DelateCardPanel>("DelateCardPanel");
    }

    private void RefreshNewCards()
    {
        drawnNewCards.Clear();
        allCards = ConvertDictionaryToList(GameDataControl.GetInstance().cardInfoDic);

        // �����ȡ���ſ���
        for (int i = 0; i < 3; i++)
        {
            float randomValue = Random.Range(0f, 1f); // ����һ��0��1֮��������

            // �����������ȷ�����Ƶ�ϡ�ж�
            if (randomValue <= rareCardPosibility)
            {
                // ���ϡ���Ƶ�ˢ�º���б�
                drawnNewCards.Add(GetRandomRareCard());
            }
            else
            {
                // �����ͨ�Ƶ�ˢ�º���б�
                drawnNewCards.Add(GetRandomEpicCard());
            }
        }

        ShowRefreshNewCards();
    }


    private void ShowRefreshNewCards()
    {
        NewcardSlot1.InitInfo(drawnNewCards[0]);
        NewcardSlot2.InitInfo(drawnNewCards[1]);
        NewcardSlot3.InitInfo(drawnNewCards[2]);
    }

    // �����п��������ѡ��һ��ϡ����
    private Card GetRandomRareCard()
    {
        List<Card> rareCards = allCards.FindAll(card => card.Rarity == "Rare" && card.Type == "ElectricalEnergy");
        return rareCards[Random.Range(0, rareCards.Count)];
    }

    // �����п��������ѡ��һ��ʷʫ��
    private Card GetRandomEpicCard()
    {
        List<Card> normalCards = allCards.FindAll(card => card.Rarity == "Epic" && card.Type == "ElectricalEnergy");
        return normalCards[Random.Range(0, normalCards.Count)];
    }


    private void SelectCard()
    {
        switch (currentSelectedNewCard)
        {
            case 0:
                //û��ѡ���κο������·���
                break;
            case 1:
                EventCenter.GetInstance().EventTrigger<int,int>("CardChange",drawnNewCards[0].CardID, 1);
                EventCenter.GetInstance().EventTrigger("CardPlusOne");
                GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard = true;
                UIManager.GetInstance().HidePanel("AwardPanel");
                break;
            case 2:
                EventCenter.GetInstance().EventTrigger<int, int>("CardChange", drawnNewCards[1].CardID, 1);
                EventCenter.GetInstance().EventTrigger("CardPlusOne");
                GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard = true;
                UIManager.GetInstance().HidePanel("AwardPanel");
                break;
            case 3:
                EventCenter.GetInstance().EventTrigger<int, int>("CardChange", drawnNewCards[2].CardID, 1);
                EventCenter.GetInstance().EventTrigger("CardPlusOne");
                GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard = true;
                UIManager.GetInstance().HidePanel("AwardPanel");
                break;
        }
    }

   


    /// <summary>
    /// ���ֵ�ת��Ϊlist�洢
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public List<Card> ConvertDictionaryToList(Dictionary<int, Card> dictionary)
    {
        List<Card> list = new List<Card>(dictionary.Values);
        return list;
    }

    public void EnterNewCardSlot1(BaseEventData data)
    {
        NewcardSlot1.gameObject.transform.localScale = new Vector3(2, 2, 2);
        currentSelectedNewCard = 1;
    }
    public void EnterNewCardSlot2(BaseEventData data)
    {
        NewcardSlot2.gameObject.transform.localScale = new Vector3(2, 2, 2);
        currentSelectedNewCard = 2;
    }
    public void EnterNewCardSlot3(BaseEventData data)
    {
        NewcardSlot3.gameObject.transform.localScale = new Vector3(2, 2, 2);
        currentSelectedNewCard = 3;
    }

    public void ExitNewCardSlot1(BaseEventData data)
    {
        NewcardSlot1.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentSelectedNewCard = 0;
    }
    public void ExitNewCardSlot2(BaseEventData data)
    {
        NewcardSlot2.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentSelectedNewCard = 0;
    }
    public void ExitNewCardSlot3(BaseEventData data)
    {
        NewcardSlot3.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        currentSelectedNewCard = 0;
    }
}
