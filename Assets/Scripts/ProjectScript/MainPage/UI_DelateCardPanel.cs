using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DelateCardPanel : BasePanel
{
    public Transform grid;
    private List<UI_CardCell> cardlist = new List<UI_CardCell>();

    public Image SelectIcon; 
    public UI_CardDetail ShowCard;
    public TMP_Text CardDelatePanelInfo;
    public TMP_Text MinimumCardNumber;
    public TMP_Text MaximumCardNumber;
    private int playerCardsSum;


    public TMP_Text CurrentSelectedCardNumber;
    public Transform SelectedIcons;
    private Card currentSelectedCard;
    private Card previousSelectedCard;
    private Vector3 currentSelectedCardPostion;
    private List<Card> cardsNeedToDelate;
    private int DelateCardTimes;

    public override void ShowMe()
    {
        base.ShowMe();
        DelateCardTimes = 3;
        ShowCards();
        ShowPlayerCardInfo();
        cardsNeedToDelate = new List<Card>();

    }

    protected override void Awake()
    {
        base.Awake();
        EventCenter.GetInstance().AddEventListener<Card,Vector3>("EnterCard", InitShowCard);
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
            //当左键点击鼠标
            SelectCardToDelateList();
        }

        if (Input.GetMouseButtonDown(1))
        {
            //当右键点击鼠标
            CancelSelectCardToDelateList();
        }
    }

    private void SelectCardToDelateList()
    {
        if(DelateCardTimes == 0)
        {
            return;
        }
        else if(currentSelectedCard == previousSelectedCard)
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

            previousSelectedCard = currentSelectedCard;
            ShowPlayerCardInfo();


            //Debug.Log(DelateCardTimes);
            Debug.Log(cardsNeedToDelate.Count);

        }
        
    }

    private void CancelSelectCardToDelateList()
    {
        if (DelateCardTimes == 3)
        {
            return;
        }
        else
        {
            RemoveChildGameObject(SelectedIcons, "Card_SelectedIcon" + (DelateCardTimes+1).ToString());
            
            DelateCardTimes += 1;
            cardsNeedToDelate.RemoveAt(3-DelateCardTimes);

            ShowPlayerCardInfo();

            Debug.Log(cardsNeedToDelate.Count);
        }
    }

    // 删除指定名字的子级
    private void RemoveChildGameObject(Transform parent, string childName)
    {
        Transform childTransform = parent.Find(childName); // 查找子级 Transform

        if (childTransform != null)
        {
            // 找到了指定名字的子级，将其销毁
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

        // 更新内容
        for (int i = 0; i < cardlist.Count; ++i)
        {
            Destroy(cardlist[i].gameObject);
        }
        cardlist.Clear();

        for (int i = 0; i < Playercards.Count; ++i)
        {
            UI_CardCell cardCell = ResMgr.GetInstance().Load<GameObject>("UI/Card_UI").GetComponent<UI_CardCell>();
            cardCell.InitInfo(Playercards[i]);
            cardCell.transform.SetParent(grid, false);
            cardlist.Add(cardCell);

        }
    }

    private void InitShowCard(Card info, Vector3 position)
    {
        ShowCard.InitInfo(info);
        SelectIcon.gameObject.SetActive(true);
        SelectIcon.transform.position = position;
        currentSelectedCardPostion = position;
        currentSelectedCard = info;
    }



    private void ShowPlayerCardInfo()
    {
        EventCenter.GetInstance().EventTrigger("SumPlayerCard");
        playerCardsSum = GameDataControl.GetInstance().PlayerDataInfo.playerCardsSum;
        CardDelatePanelInfo.text = "当前卡牌数量：" + playerCardsSum;
        MinimumCardNumber.text = "最低卡牌数量：20";
        MaximumCardNumber.text = "最高卡牌数量：28";
        CurrentSelectedCardNumber.text = "还可以从牌组中删除 " + DelateCardTimes + "/3 张卡牌";
    }
}
