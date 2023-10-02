using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class UI_DelateCardPanel : BasePanel
{
    public Transform grid;
    private List<UI_CardCell> cardlist = new List<UI_CardCell>();

    public Image SelectIcon; 
    public UI_CardDetail ShowCard;
    public TMP_Text CardDelatePanelInfo;


    public TMP_Text CurrentSelectedCardNumber;
    public Transform SelectedIcons;
    private Card currentSelectedCard;
    private Vector3 previousSelectedCardPosition;
    private Vector3 currentSelectedCardPostion;
    private List<Card> cardsNeedToDelate;
    private int DelateCardTimes;
    public Image CancelArea;

    private bool CanAddDelatedCard = false;


    public override void ShowMe()
    {
        base.ShowMe();
        DelateCardTimes = 3;
        ShowCards();
        ShowPlayerCardInfo();
        cardsNeedToDelate = new List<Card>();
        previousSelectedCardPosition = new Vector3(0, 0, 0);
        currentSelectedCardPostion = new Vector3(0, 0, 0);
        currentSelectedCard = new Card();

        GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_DelateCard = false;

    }

    protected override void Awake()
    {
        base.Awake();
        EventCenter.GetInstance().AddEventListener<Card,Vector3>("EnterCard", InitShowCard);

        GetControl<Button>("ButtonNextStep").onClick.AddListener(FinishDelate);

        //����������������Ƴ����¼������д���
        EventTrigger trigger = CancelArea.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(MouseEnterCancelArea);

        trigger.triggers.Add(enter);
    }

    public void MouseEnterCancelArea(BaseEventData data)
    {
        CancelSelectedIcon();
    }

    public override void HideMe()
    {
        base.HideMe();
        EventCenter.GetInstance().RemoveEventListener<Card,Vector3>("EnterCard", InitShowCard);

        cardsNeedToDelate.Clear();
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //�����������
            SelectCardToDelateList();
        }

        if (Input.GetMouseButtonDown(1))
        {
            //���Ҽ�������
            CancelSelectCardToDelateList();
        }
    }

    private void FinishDelate()
    {
        if(GameDataControl.GetInstance().PlayerDataInfo.playerCardsSum <= 28 && GameDataControl.GetInstance().PlayerDataInfo.playerCardsSum >= 20)
        {
            Debug.Log(cardsNeedToDelate.Count);

            GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_DelateCard = true;
            for(int i = 0; i < cardsNeedToDelate.Count; i++)
            {
                Debug.Log(i);
                EventCenter.GetInstance().EventTrigger<int, int>("CardChange", cardsNeedToDelate[i].CardID, -1);
            }

            UIManager.GetInstance().HidePanel("DelateCardPanel");
            UIManager.GetInstance().HidePanel("AwardPanel");
        }
    }

    private void SelectCardToDelateList()
    {
        if(DelateCardTimes == 0)
        {
            return;
        }
        else if(currentSelectedCardPostion == previousSelectedCardPosition)
        {
            return;
        }
        else if (!CanAddDelatedCard)
        {
            return;
        }
        else
        {
            GameObject slectedCardIcon = ResMgr.GetInstance().Load<GameObject>("UI/Card_SelectedIcon");
            slectedCardIcon.transform.position = currentSelectedCardPostion;
            slectedCardIcon.transform.SetParent(SelectedIcons);
            slectedCardIcon.SetActive(true);
            slectedCardIcon.name = "Card_SelectedIcon" + DelateCardTimes.ToString();
            DelateCardTimes -= 1;
            cardsNeedToDelate.Add(currentSelectedCard);

            previousSelectedCardPosition = currentSelectedCardPostion;

            GameDataControl.GetInstance().PlayerDataInfo.playerCardsSum -= 1;
            ShowPlayerCardInfo();


            //Debug.Log(DelateCardTimes);
            Debug.Log(cardsNeedToDelate.Count);

        }
        
    }

    private void CancelSelectCardToDelateList()
    {
        if (DelateCardTimes == 3)
        {
            previousSelectedCardPosition = new Vector3(0, 0, 0);
            return;
        }
        else
        {
            RemoveChildGameObject(SelectedIcons, "Card_SelectedIcon" + (DelateCardTimes+1).ToString());
            
            DelateCardTimes += 1;
            cardsNeedToDelate.RemoveAt(3-DelateCardTimes);

            GameDataControl.GetInstance().PlayerDataInfo.playerCardsSum += 1;
            ShowPlayerCardInfo();

            Debug.Log(cardsNeedToDelate.Count);
        }
    }

    // ɾ��ָ�����ֵ��Ӽ�
    private void RemoveChildGameObject(Transform parent, string childName)
    {
        Transform childTransform = parent.Find(childName); // �����Ӽ� Transform

        if (childTransform != null)
        {
            // �ҵ���ָ�����ֵ��Ӽ�����������
            Destroy(childTransform.gameObject);
        }
        else
        {
            return ;
        }
    }


    private void ShowCards()
    {
        List<Card> Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

        // ��������
        for (int i = 0; i < cardlist.Count; ++i)
        {
            Destroy(cardlist[i].gameObject);
        }
        cardlist.Clear();

        // ���ɿ��ƶ�Ӧ������ cardCell
        foreach (Card playerCard in Playercards)
        {
            for (int i = 0; i < playerCard.PlayerOwnedNumber; i++)
            {
                UI_CardCell cardCell = ResMgr.GetInstance().Load<GameObject>("UI/Card_UI").GetComponent<UI_CardCell>();
                cardCell.InitInfo(playerCard);
                cardCell.transform.SetParent(grid, false);
                cardCell.cardNmberImage.enabled = false;
                cardCell.CardNumber.enabled = false;
                cardlist.Add(cardCell);
            }
        }
    }

    private void InitShowCard(Card info, Vector3 position)
    {
        ShowCard.InitInfo(info);
        SelectIcon.gameObject.SetActive(true);
        SelectIcon.transform.position = position;
        currentSelectedCardPostion = position;
        currentSelectedCard = info;

        CanAddDelatedCard = true;
    }

    private void CancelSelectedIcon()
    {
        SelectIcon.gameObject.SetActive(false);
        currentSelectedCard = new Card();

        CanAddDelatedCard = false;
    }


    private void ShowPlayerCardInfo()
    {
        CardDelatePanelInfo.text = "��ǰ��������" + GameDataControl.GetInstance().PlayerDataInfo.playerCardsSum + ", ������Ӧ����20��28֮��";
        CurrentSelectedCardNumber.text = "�����Դ�������ɾ�� " + DelateCardTimes + "/3 �ſ���";
    }
}
