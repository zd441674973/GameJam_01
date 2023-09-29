using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_CardCell : BasePanel
{
    private Card cardCellInfo;
    public TMP_Text cardName;
    public TMP_Text CardNumber;

    public override void ShowMe()
    {
        base.ShowMe();
        
    }

    protected override void Awake()
    {
        base.Awake();
        //监听鼠标移入和鼠标移出的事件，进行处理
        EventTrigger trigger = GetControl<Image>("Image_Cover").gameObject.AddComponent<EventTrigger>();

        //申明一个鼠标进入的事件类对象
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(MouseEnterCardCell);

        //申明一个鼠标移出的事件类对象
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(MouseExitCardCell);

        trigger.triggers.Add(enter);
        trigger.triggers.Add(exit);
    }

    public void MouseEnterCardCell(BaseEventData data)
    {
        if (cardCellInfo == null)
            return;
        //显示面板
        UIManager.GetInstance().ShowPanel<UI_CardDetail>("Card_Detail", E_UI_Layer.Top, (panel) =>
        {
            //异步加载结束后，设置位置信息
            //更新信息
            panel.InitInfo(cardCellInfo);
            //更新位置
            panel.transform.position = GetControl<Image>("Image_Cover").transform.position;
        });
    }
    public void MouseExitCardCell(BaseEventData data)
    {
        if (cardCellInfo == null)
            return;
        //隐藏面板
        UIManager.GetInstance().HidePanel("Card_Detail");
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
        //GetControl<Image>("Image_Card").sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + itemData.Icon);
        //卡牌名字
        cardName.text = cardData.CardName;
        //数量
        CardNumber.text = info.PlayerOwnedNumber.ToString();

    }
}
