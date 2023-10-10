using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CardCell : BasePanel
{
    private Card cardCellInfo;
    public TMP_Text cardName;
    public TMP_Text CardNumber;
    public Image cover;
    public Image cardNmberImage;
    public TMP_Text cardDes; 

    public override void ShowMe()
    {
        base.ShowMe();
        
    }

    protected override void Awake()
    {
        base.Awake();
        //监听鼠标移入和鼠标移出的事件，进行处理
        EventTrigger trigger = cover.gameObject.AddComponent<EventTrigger>();

        //申明一个鼠标进入的事件类对象
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(MouseEnterCardCell);

        trigger.triggers.Add(enter);
       
    }

    public void MouseEnterCardCell(BaseEventData data)
    {
        if (cardCellInfo == null)
            return;
        EventCenter.GetInstance().EventTrigger<Card, Vector3>("EnterCard", cardCellInfo, this.transform.position);
    }
  
    /// <summary>
    /// 初始化卡牌
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(Card info)
    {
        this.cardCellInfo = info;
        //根据id得到卡牌信息
        Card cardData = GameDataControl.GetInstance().GetCardInfo(info.CardID);

        //Card cardData = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards[info.CardID];
        //卡牌图案
        GetControl<Image>("Image_Card").sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + info.CardName);
        //卡牌名字
        cardName.text = cardData.CardName;
        //数量
        CardNumber.text = info.PlayerOwnedNumber.ToString();

        cardDes.text = info.Description;

    }
}
