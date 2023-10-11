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
        //����������������Ƴ����¼������д���
        EventTrigger trigger = cover.gameObject.AddComponent<EventTrigger>();

        //����һ����������¼������
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
    /// ��ʼ������
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(Card info)
    {
        this.cardCellInfo = info;
        //����id�õ�������Ϣ
        Card cardData = GameDataControl.GetInstance().GetCardInfo(info.CardID);

        //Card cardData = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards[info.CardID];
        //����ͼ��
        GetControl<Image>("Image_Card").sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + info.CardName);
        //��������
        cardName.text = cardData.CardName;
        //����
        CardNumber.text = info.PlayerOwnedNumber.ToString();

        cardDes.text = info.Description;

    }
}
