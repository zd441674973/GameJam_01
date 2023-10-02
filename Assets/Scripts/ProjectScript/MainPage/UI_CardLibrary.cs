using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;
using System.Linq;

public class UI_CardLibrary : BasePanel
{
    public Transform grid;
    private List<UI_CardCell> cardlist = new List<UI_CardCell>();
    public UI_CardDetail ShowCard;
    public TMP_Text CardLabInfo;
    public TMP_Text MinimumCardNumber;
    public TMP_Text MaximumCardNumber;

    public override void ShowMe()
    {
        base.ShowMe();
        ShowCards();
        ShowPlayerCardInfo();
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
    }

  

    private void ShowPlayerCardInfo()
    {
        EventCenter.GetInstance().EventTrigger("SumPlayerCard");
        CardLabInfo.text = "当前卡牌数量：" + GameDataControl.GetInstance().PlayerDataInfo.playerCardsSum;
        MinimumCardNumber.text = "最低卡牌数量：20";
        MaximumCardNumber.text = "最高卡牌数量：28";
    }
}
